using System;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api.Domain
{
    public class Despesa : BaseEntity
    {
        public long DeputadoId { get; set; }
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
    }
}