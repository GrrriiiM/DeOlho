namespace DeOlho.Services.Politicos.Api.Domain
{
    public class PartidoContato : Entity
    {
        protected PartidoContato()
        {

        }

        public PartidoContato(
            Partido partido,
            ContatoTipo tipo,
            string contato)
            : this()
        {
            Partido = partido;
            PartidoId = partido.Id;
            Tipo = tipo;
            Contato = contato;
        }

        public Partido Partido { get; protected set; }
        public long PartidoId { get; protected set; }
        public ContatoTipo Tipo { get; protected set; }
        public string Contato { get; set; }
    }
}