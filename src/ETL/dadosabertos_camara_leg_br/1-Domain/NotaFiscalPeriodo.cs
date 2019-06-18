using System.Collections.Generic;
using DeOlho.ETL.dadosabertos_leg_br.Domain.SeedWork;

namespace DeOlho.ETL.dadosabertos_leg_br.Domain
{
    public class NotaFiscalPeriodo : Entity
    {
        public NotaFiscalPeriodo(DeputadoFederal deputadoFederal, int ano, int mes)
        {
            DeputadoFederal = deputadoFederal;
            DeputadoFederalId = deputadoFederal.Id;
            Ano = ano;
            Mes = mes;
        }

        public DeputadoFederal DeputadoFederal { get; set; }
        public long DeputadoFederalId { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }

        public List<NotaFiscal> _notasFiscais = new List<NotaFiscal>(); 
        public IReadOnlyCollection<NotaFiscal> NotasFiscais => _notasFiscais.AsReadOnly(); 

        public NotaFiscal AddNotaFiscal(dynamic registroImportacao)
        {
            var notaFiscal = new NotaFiscal(this, registroImportacao);
            _notasFiscais.Add(notaFiscal);
            return notaFiscal;
        }
    }
}