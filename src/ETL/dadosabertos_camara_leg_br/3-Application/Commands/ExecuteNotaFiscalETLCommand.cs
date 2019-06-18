using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_leg_br.Domain;
using DeOlho.ETL.dadosabertos_leg_br.Infrastructure.Repositories;
using DeOlho.ETL.Sources;
using DeOlho.ETL.Transforms;
using MediatR;

namespace DeOlho.ETL.dadosabertos_leg_br.Application.Commands
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
                .Extract(async () => await _deputadoFederalRepository.ListAllAsync(cancellationToken))
                .TransformToList(_ => _.Value)
                .AsParallel()
                .WithDegreeOfParallelism(4)
                .Extract(_ => new HttpJsonSource(this._httpClientFactory.CreateClient(), string.Format(_configuration.DeputadoFederalNotaFiscalUrl, _.Value.OrigemId, request.Ano, request.Mes)))
                .TransformJsonToDynamic()
                .TransformToList(_ => new List<dynamic>(_.Value.dados))
                .Where(_ => _.Value == null)
                .Transform(_ => {
                    var deputadoFederal = _.GetParent<DeputadoFederal>().Value;
                    var notasFiscaisPeriodos = deputadoFederal.NotasFiscaisPeriodos.SingleOrDefault(_1 => _1.Ano == request.Ano && _1.Mes == request.Mes);
                    
                    if (notasFiscaisPeriodos == null)
                        notasFiscaisPeriodos = deputadoFederal.AddNotaFiscalPeriodos(request.Ano, request.Mes);

                    var notaFiscal = notasFiscaisPeriodos.NotasFiscais.SingleOrDefault(_1 => _1.CnpjCpfFornecedor == (string)_.Value.cnpjCpfFornecedor && _1.NumDocumento == (string)_.Value.numDocumento);

                    if (notaFiscal == null)
                        notaFiscal = notasFiscaisPeriodos.AddNotaFiscal(_.Value);
                    else
                        notaFiscal.Update(_.Value);
                    
                    return deputadoFederal;
                })
                .Execute();
            var deputadosFederais = result.Select(_ => _.Value).Distinct();
            await _deputadoFederalRepository.AddRangeAsync(deputadosFederais, cancellationToken);
            await _deputadoFederalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new Unit();
        }
    }
}