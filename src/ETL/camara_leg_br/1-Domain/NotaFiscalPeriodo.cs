using System;
using System.Collections.Generic;
using System.Linq;
using DeOlho.ETL.camara_leg_br.Domain.SeedWork;

namespace DeOlho.ETL.camara_leg_br.Domain
{
    public class NotaFiscalPeriodo : Entity
    {
        protected NotaFiscalPeriodo(){}
        public NotaFiscalPeriodo(DeputadoFederal deputadoFederal, int ano, int mes)
        {
            DeputadoFederal = deputadoFederal;
            DeputadoFederalId = deputadoFederal.Id;
            Ano = ano;
            Mes = mes;
        }

        public virtual DeputadoFederal DeputadoFederal { get; private set; }
        public long DeputadoFederalId { get; private set; }
        public int Ano { get; private set; }
        public int Mes { get; private set; }

        public List<NotaFiscal> _notasFiscais = new List<NotaFiscal>(); 
        public virtual IReadOnlyCollection<NotaFiscal> NotasFiscais => _notasFiscais.AsReadOnly(); 

        public NotaFiscal AddNotaFiscalOrUpdateIfExist(dynamic registro)
        {
            if ((int)registro.ano != Ano) throw new DomainException("Ano do registro diferente do ano do período");
            if ((int)registro.mes != Mes) throw new DomainException("Mês do registro diferente do mês do período");

            var notafiscal = NotasFiscais.SingleOrDefault(_ => 
                _.CodDocumento == (long)registro.codDocumento
                && _.CnpjCpfFornecedor == (string)registro.cnpjCpfFornecedor 
                && _.CodTipoDocumento == (int)registro.codTipoDocumento 
                && _.DataDocumento == (DateTime)registro.dataDocumento
                && _.NumDocumento == (string)registro.numDocumento);

            if (notafiscal != null)
            {
                if (notafiscal.HasChange(registro))
                {
                    notafiscal.Update(registro);
                }
                return notafiscal;
            }

            var notaFiscal = new NotaFiscal(this, registro);
            _notasFiscais.Add(notaFiscal);
            return notaFiscal;
        }
    }
}