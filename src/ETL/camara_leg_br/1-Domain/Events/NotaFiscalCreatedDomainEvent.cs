using MediatR;

namespace DeOlho.ETL.camara_leg_br.Domain
{
    public class NotaFiscalCreatedDomainEvent : INotification
    {
        public NotaFiscal NotaFiscal { get; private set; }

        public NotaFiscalCreatedDomainEvent(NotaFiscal notaFiscal)
        {
            NotaFiscal = notaFiscal;
        }
    }
}