using System.Collections.Generic;
using DeOlho.ETL.dadosabertos_leg_br.Domain.SeedWork;

namespace DeOlho.ETL.dadosabertos_leg_br.Domain
{
    public class DeputadoFederal : Entity
    {
        private DeputadoFederal() {} 
        public DeputadoFederal(dynamic registro)
        {
            CPF = (long)registro.cpf;
            Update(registro);
        }

        
        public long CPF { get; set; }
        public long OrigemId { get; set; }
        public string URLWebsite { get; set; }
        public string URLFoto { get; set; }
        public string CondicaoEleitoral { get; set; }
        public string GabineteNome { get; set; }
        public string GabinetePredio { get; set; }
        public string GabineteSala { get; set; }
        public string GabineteAndar { get; set; }
        public string GabineteTelefone { get; set; }
        public string GabineteEmail { get; set; }

        public List<NotaFiscalPeriodo> _notasFiscaisPeriodos = new List<NotaFiscalPeriodo>();
        public IReadOnlyCollection<NotaFiscalPeriodo> NotasFiscaisPeriodos => _notasFiscaisPeriodos.AsReadOnly(); 

        public NotaFiscalPeriodo AddNotaFiscalPeriodos(int ano, int mes)
        {
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