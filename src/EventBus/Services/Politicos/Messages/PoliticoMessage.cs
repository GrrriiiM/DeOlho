using System;

namespace DeOlho.EventBus.Services.Politicos.Messages
{
    public class PoliticoMessage : BaseMessage
    {
        public long Id { get; set; }
        public string CPF { get; set; }
        public DateTime? DataFalecimento { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Escolaridade { get; set; }
        public string MunicipioNascimento { get; set; }
        public string NomeCivil { get; set; }
        public string RedeSocial { get; set; }
        public string Sexo { get; set; }
        public string UFNascimento { get; set; }
        public string URLWebsite { get; set; }
        public long LegislaturaId  { get; set; }
        public string Nome { get; set; }
        public string NomeEleitoral { get; set; }
        public string SiglaPartido { get; set; }
        public string SiglaUf { get; set; }
        public string Situacao { get; set; }
        public long PartidoId { get; set; }
        public string URLFoto { get; set; }
        public string CondicaoEleitoral { get; set; }
        public DateTime? Data { get; set; }
        public string DescricaoStatus { get; set; }
        public string GabineteAndar { get; set; }
        public string GabineteEmail { get; set; }
        public string GabineteNome { get; set; }
        public string GabinetePredio { get; set; }
        public string GabineteSala { get; set; }
        public string GabineteTelefone { get; set; }
    }
}