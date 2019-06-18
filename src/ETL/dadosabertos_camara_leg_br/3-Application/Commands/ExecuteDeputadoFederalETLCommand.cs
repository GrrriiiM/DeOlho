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
    public class ExecuteDeputadoFederalETLCommand : IRequest
    {
        
    }

    public class ExecuteDeputadoFederalETLCommandHandler : IRequestHandler<ExecuteDeputadoFederalETLCommand>
    {
        readonly ETLConfiguration _configuration;
        readonly IDeputadoFederalRepository _deputadoFederalRepository;
        readonly IHttpClientFactory _httpClientFactory;

        public ExecuteDeputadoFederalETLCommandHandler(
            ETLConfiguration configuration,
            IDeputadoFederalRepository deputadoFederalRepository,
            IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _deputadoFederalRepository = deputadoFederalRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Unit> Handle(ExecuteDeputadoFederalETLCommand request, CancellationToken cancellationToken)
        {
            var result = await new Process()
                .Extract(() => new HttpJsonSource(_httpClientFactory.CreateClient(), this._configuration.DeputadoFederalUrl))
                .TransformJsonToDynamic()
                .TransformToList(_ => new List<dynamic>(_.Value.dados))
                .AsParallel()
                .WithDegreeOfParallelism(4)
                .Extract(_ => new HttpJsonSource(this._httpClientFactory.CreateClient(), string.Format(this._configuration.DeputadoFederalDetailUrl, _.Value.id)))
                .TransformJsonToDynamic()
                .Transform(_ => _.Value.dados)
                .Transform(_ => (DeputadoFederal)_deputadoFederalRepository.FindByCPF((long)_.Value.cpf))
                .Where(_ => _.Value == null || _.Value.HasChange((dynamic)_.Parent.Value))
                .Transform(_ => {
                    if (_.Value == null)
                        return new DeputadoFederal(_.Parent.Value);
                    else {
                        _.Value.Update((dynamic)_.Parent.Value);
                        return _.Value;
                    }
                })
                .Execute();
            var deputadosFederais = result.Select(_ => _.Value);
            await _deputadoFederalRepository.AddRangeAsync(deputadosFederais, cancellationToken);
            await _deputadoFederalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return new Unit();
        }
    }
}