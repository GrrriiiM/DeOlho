namespace DeOlho.Services.Politicos.Api.Domain
{
    public class PoliticoContato : Entity
    {
        protected PoliticoContato()
        {

        }
        
        public PoliticoContato(
            Politico politico,
            ContatoTipo tipo,
            string url)
            : this()
        {
            Politico = politico;
            PoliticoId = politico.Id;
            Tipo = tipo;
            Contato = url;
        }

        public Politico Politico { get; protected set; }
        public long PoliticoId { get; protected set; }
        public ContatoTipo Tipo { get; protected set; }
        public string Contato { get; set; }
    }
}