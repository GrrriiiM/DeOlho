namespace DeOlho.Services.Politicos.Api.Domain
{
    public class PoliticoMandato : Entity
    {
        protected PoliticoMandato()
        {

        }

        public PoliticoMandato(
            Politico politico,
            Mandato mandato,
            PoliticoMandatoSituacao situacao)
            : this()
        {
            Politico = politico;
            PoliticoId = politico.Id;
            Mandato = mandato;
            MandatoId = mandato.Id;
            Situacao = situacao;
        }

        public Politico Politico { get; protected set; }
        public long PoliticoId { get; protected set; }
        public Mandato Mandato { get; protected set; }
        public long MandatoId { get; protected set; }
        public PoliticoMandatoSituacao Situacao { get; set; }
    }
}