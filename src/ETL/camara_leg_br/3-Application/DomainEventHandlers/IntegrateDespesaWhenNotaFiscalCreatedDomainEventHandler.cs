using System.Threading;
using System.Threading.Tasks;
using DeOlho.ETL.camara_leg_br.Domain;
using DeOlho.EventBus.Services.Despesas.Messages;
using MediatR;
using RawRabbit;

namespace DeOlho.ETL.camara_leg_br.Application.DomainEventHandlers
{
    public class IntegrateDespesaWhenNotaFiscalCreatedDomainEventHandler : INotificationHandler<NotaFiscalCreatedDomainEvent>
    {
        readonly IBusClient _busClient;
        public IntegrateDespesaWhenNotaFiscalCreatedDomainEventHandler(
            IBusClient busClient)
        {
            _busClient = busClient;
        }
        public async Task Handle(NotaFiscalCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var message = new DespesaCreatedMessage
            {
                CPF = notification.NotaFiscal.Periodo.DeputadoFederal.CPF,
                TipoId = 1,
                Ano = notification.NotaFiscal.Periodo.Ano,
                Mes = notification.NotaFiscal.Periodo.Mes,
                Valor = notification.NotaFiscal.ValorDocumento
            };

            await _busClient.PublishAsync(message);
        }
    }
}