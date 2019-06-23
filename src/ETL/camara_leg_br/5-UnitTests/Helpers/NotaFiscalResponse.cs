using System;

namespace DeOlho.ETL.camara_leg_br.UnitTests.Helpers
{
    public class NotaFiscalResponse
    {
        public int ano;
        public int mes;
        public long codDocumento;
        public string cnpjCpfFornecedor;
        public long codLote;
        public int codTipoDocumento;
        public DateTime dataDocumento;
        public string nomeFornecedor;
        public string numDocumento;
        public string numRessarcimento;
        public int parcela;
        public string tipoDespesa;
        public string tipoDocumento;
        public string urlDocumento;
        public decimal valorDocumento;
        public decimal valorGlosa;
        public decimal valorLiquido;
    }
}