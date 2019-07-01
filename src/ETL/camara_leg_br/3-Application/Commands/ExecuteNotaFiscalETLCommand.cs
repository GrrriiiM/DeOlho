using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DeOlho.ETL.camara_leg_br.Domain;
using DeOlho.ETL.camara_leg_br.Infrastructure.Repositories;
using DeOlho.ETL.Sources;
using DeOlho.ETL.Transforms;
using MediatR;

namespace DeOlho.ETL.camara_leg_br.Application.Commands
{
    public class ExecuteNotaFiscalETLCommand : IRequest
    {
        public int Ano { get; set; } 
        public int Mes { get; set; }
    }

    public class ExecuteNotaFiscalETLCommandHandler : IRequestHandler<ExecuteNotaFiscalETLCommand>
    {
        readonly ETLConfiguration _configuration;
        readonly IDeputadoFederalRepository _deputadoFederalRepository;
        readonly IHttpClientFactory _httpClientFactory;

        public ExecuteNotaFiscalETLCommandHandler(
            ETLConfiguration configuration,
            IDeputadoFederalRepository deputadoFederalRepository,
            IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _deputadoFederalRepository = deputadoFederalRepository;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Unit> Handle(ExecuteNotaFiscalETLCommand request, CancellationToken cancellationToken)
        {
            var result = await new Process()
                .Extract(async () => (await _deputadoFederalRepository.ListAllAsync(cancellationToken)))
                .TransformToList(_ => _.Value)
                .AsParallel()
                .WithDegreeOfParallelism(4)
                .Extract(_ => new HttpJsonSource(this._httpClientFactory.CreateClient(), string.Format(_configuration.DeputadoFederalNotaFiscalUrl, _.Value.OrigemId, request.Ano, request.Mes)))
                .TransformJsonToDynamic()
                .TransformToList(_ => new List<dynamic>(_.Value.dados))
                .Transform(_ => {
                    var deputadoFederal = _.GetParent<DeputadoFederal>().Value;
                    
                    var notasFiscaisPeriodos = deputadoFederal.AddNotaFiscalPeriodosIfNotExist(request.Ano, request.Mes);

                    notasFiscaisPeriodos.AddNotaFiscalOrUpdateIfExist(_.Value);
                    
                    return deputadoFederal;
                })
                .Execute();
            var deputadosFederais = result.Select(_ => _.Value).Distinct();

            // foreach(var deputadoFederal in deputadosFederais)
            // {
            //     _deputadoFederalRepository.Update(deputadoFederal);
                await _deputadoFederalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            //}
            
            

            return new Unit();
        }
    }
}