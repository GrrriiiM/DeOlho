using System.Threading;
using System.Threading.Tasks;
using DeOlho.ETL.tse_jus_br.Api.Domain.Events;
using DeOlho.EventBus.Services.Politicos.Messages;
using MediatR;
using RawRabbit;

namespace Api.Application.DomainEvents
{
    public class PublishIntegrationServiceWhenPoliticoCreatedEventHandler : INotificationHandler<PoliticoCreatedDomainEvent>
    {

        readonly IBusClient _busClient;

        public PublishIntegrationServiceWhenPoliticoCreatedEventHandler(
            IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task Handle(PoliticoCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var message = new PoliticoChangeMessage
            {
                Apelido = notification.NM_URNA_CANDIDATO,
                CPF = notification.NR_CPF_CANDIDATO.ToString(),
                Escolaridade = 1,
                Falecimento = null,
                MandatoFim = notification.DT_ELEICAO.AddYears(4),
                MandatoInicio = notification.DT_ELEICAO,
                MandatoSituacao = 1,
                Nascimento = notification.DT_NASCIMENTO,
                MandatoTipo = 1,
                NascimentoMunicipio = notification.NM_MUNICIPIO_NASCIMENTO,
                NascimentoUF = notification.SG_UF_NASCIMENTO,
                Nome = notification.NM_CANDIDATO,
                Partido = notification.SG_PARTIDO,
                Sexo = 1
            };

            await _busClient.PublishAsync(message);
        }
    }
}