using System.Collections.Generic;
using System.Linq;
using DeOlho.ETL.camara_leg_br.Domain.SeedWork;

namespace DeOlho.ETL.camara_leg_br.Domain
{
    public class DeputadoFederal : Entity
    {
        protected DeputadoFederal() {} 
        public DeputadoFederal(dynamic registro)
        {
            CPF = (long)registro.cpf;
            Update(registro);
        }

        
        public long CPF { get; private set; }
        public long OrigemId { get; private set; }
        public string URLWebsite { get; private set; }
        public string URLFoto { get; private set; }
        public string CondicaoEleitoral { get; private set; }
        public string GabineteNome { get; private set; }
        public string GabinetePredio { get; private set; }
        public string GabineteSala { get; private set; }
        public string GabineteAndar { get; private set; }
        public string GabineteTelefone { get; private set; }
        public string GabineteEmail { get; private set; }

        public List<NotaFiscalPeriodo> _notasFiscaisPeriodos = new List<NotaFiscalPeriodo>();
        public virtual IReadOnlyCollection<NotaFiscalPeriodo> NotasFiscaisPeriodos => _notasFiscaisPeriodos.AsReadOnly(); 

        public NotaFiscalPeriodo AddNotaFiscalPeriodosIfNotExist(int ano, int mes)
        {
            var notasFiscaisPeriodosExist = NotasFiscaisPeriodos.SingleOrDefault(_ => _.Ano == ano && _.Mes == mes);
            if (notasFiscaisPeriodosExist != null)
                return notasFiscaisPeriodosExist;
            var notaFiscalPeriodo = new NotaFiscalPeriodo(this, ano, mes);
            _notasFiscaisPeriodos.Add(notaFiscalPeriodo);
            return notaFiscalPeriodo;
        }

        public bool HasChange(dynamic registro)
        {
            return OrigemId != (long)registro.id 
                || URLWebsite != (string)registro.urlWebsite
                || URLFoto != (string)registro.ultimoStatus.urlFoto
                || CondicaoEleitoral != (string)registro.ultimoStatus.condicaoEleitoral
                || GabineteAndar != (string)registro.ultimoStatus.gabinete.andar
                || GabineteEmail != (string)registro.ultimoStatus.gabinete.email
                || GabineteNome != (string)registro.ultimoStatus.gabinete.nome
                || GabinetePredio != (string)registro.ultimoStatus.gabinete.predio
                || GabineteSala != (string)registro.ultimoStatus.gabinete.sala
                || GabineteTelefone != (string)registro.ultimoStatus.gabinete.telefone;
        }

        public void Update(dynamic registro)
        {
            OrigemId = (long)registro.id;
            URLWebsite = (string)registro.urlWebsite;
            URLFoto = (string)registro.ultimoStatus.urlFoto;
            CondicaoEleitoral = (string)registro.ultimoStatus.condicaoEleitoral;
            GabineteAndar = (string)registro.ultimoStatus.gabinete.andar;
            GabineteEmail = (string)registro.ultimoStatus.gabinete.email;
            GabineteNome = (string)registro.ultimoStatus.gabinete.nome;
            GabinetePredio = (string)registro.ultimoStatus.gabinete.predio;
            GabineteSala = (string)registro.ultimoStatus.gabinete.sala;
            GabineteTelefone = (string)registro.ultimoStatus.gabinete.telefone;
        }
    }
}