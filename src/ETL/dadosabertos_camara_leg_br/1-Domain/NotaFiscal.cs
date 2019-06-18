using System;
using DeOlho.ETL.dadosabertos_leg_br.Domain.SeedWork;

namespace DeOlho.ETL.dadosabertos_leg_br.Domain
{
    public class NotaFiscal : Entity
    {
        private NotaFiscal() {}

        public NotaFiscal(NotaFiscalPeriodo periodo, dynamic registro)
        {
            Periodo = periodo;
            PeriodoId = periodo.Id;
            Update(registro);
        }
        public long CPF { get; set; }
        public NotaFiscalPeriodo Periodo { get; set; }
        public long PeriodoId { get; set; }
        public int Ano { get; set; }
        public string CnpjCpfFornecedor { get; set; }
        public long CodDocumento { get; set; }
        public long CodLote { get; set; }
        public int? CodTipoDocumento { get; set; }
        public DateTime? DataDocumento { get; set; }
        public int Mes { get; set; }
        public string NomeFornecedor { get; set; }
        public string NumDocumento { get; set; }
        public string NumRessarcimento { get; set; }
        public int Parcela { get; set; }
        public string TipoDespesa { get; set; }
        public string TipoDocumento { get; set; }
        public string URLDocumento { get; set; }
        public decimal ValorDocumento { get; set; }
        public decimal ValorGlosa { get; set; }
        public decimal ValorLiquido { get; set; }

        public void Update(dynamic registro)
        {
            CodDocumento = (long)registro.codDocumento;
            Ano = (int)registro.ano;
            CnpjCpfFornecedor = (string)registro.cnpjCpfFornecedor;
            CodDocumento = (long)registro.codDocumento;
            CodLote = (long)registro.codLote;
            CodTipoDocumento = (int?)registro.codTipoDocumento;
            DataDocumento = (DateTime?)registro.dataDocumento;
            Mes = (int)registro.mes;
            NomeFornecedor = (string)registro.nomeFornecedor;
            NumDocumento = (string)registro.numDocumento;
            NumRessarcimento = (string)registro.numRessarcimento;
            Parcela = (int)registro.parcela;
            TipoDespesa = (string)registro.tipoDespesa;
            TipoDocumento = (string)registro.tipoDocumento;
            URLDocumento = (string)registro.urlDocumento;
            ValorDocumento = (decimal)registro.valorDocumento;
            ValorGlosa = (decimal)registro.valorGlosa;
            ValorLiquido = (decimal)registro.valorLiquido;
        }
    }
}