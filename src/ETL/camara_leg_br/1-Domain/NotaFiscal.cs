using System;
using DeOlho.ETL.camara_leg_br.Domain.SeedWork;

namespace DeOlho.ETL.camara_leg_br.Domain
{
    public class NotaFiscal : Entity
    {
        protected NotaFiscal() {}

        public NotaFiscal(NotaFiscalPeriodo periodo, dynamic registro)
        {
            Periodo = periodo;
            PeriodoId = periodo.Id;
            CodDocumento = (long)registro.codDocumento;
            CnpjCpfFornecedor = (string)registro.cnpjCpfFornecedor;
            CodTipoDocumento = (int)registro.codTipoDocumento;
            DataDocumento = (DateTime)registro.dataDocumento;
            NumDocumento = (string)registro.numDocumento;
            
            Update(registro);
        }
        public long CPF { get; private set; }
        public virtual NotaFiscalPeriodo Periodo { get; private set; }
        public long PeriodoId { get; private set; }
        public string CnpjCpfFornecedor { get; private set; }
        public long CodDocumento { get; private set; }
        public long CodLote { get; private set; }
        public int CodTipoDocumento { get; private set; }
        public DateTime DataDocumento { get; private set; }
        public string NomeFornecedor { get; private set; }
        public string NumDocumento { get; private set; }
        public string NumRessarcimento { get; private set; }
        public int Parcela { get; private set; }
        public string TipoDespesa { get; private set; }
        public string TipoDocumento { get; private set; }
        public string URLDocumento { get; private set; }
        public decimal ValorDocumento { get; private set; }
        public decimal ValorGlosa { get; private set; }
        public decimal ValorLiquido { get; private set; }

        public void Update(dynamic registro)
        {
            CodLote = (long)registro.codLote;
            NomeFornecedor = (string)registro.nomeFornecedor;
            NumRessarcimento = (string)registro.numRessarcimento;
            Parcela = (int)registro.parcela;
            TipoDespesa = (string)registro.tipoDespesa;
            TipoDocumento = (string)registro.tipoDocumento;
            URLDocumento = (string)registro.urlDocumento;
            ValorDocumento = (decimal)registro.valorDocumento;
            ValorGlosa = (decimal)registro.valorGlosa;
            ValorLiquido = (decimal)registro.valorLiquido;
        }

        public bool HasChange(dynamic registro)
        {
            return CodLote != (long)registro.codLote
                || NomeFornecedor != (string)registro.nomeFornecedor
                || NumRessarcimento != (string)registro.numRessarcimento
                || Parcela != (int)registro.parcela
                || TipoDespesa != (string)registro.tipoDespesa
                || TipoDocumento != (string)registro.tipoDocumento
                || URLDocumento != (string)registro.urlDocumento
                || ValorDocumento != (decimal)registro.valorDocumento
                || ValorGlosa != (decimal)registro.valorGlosa
                || ValorLiquido != (decimal)registro.valorLiquido;
        }
    }
}