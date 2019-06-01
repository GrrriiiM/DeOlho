using System.Collections.Generic;

namespace DeOlho.Services.Politicos.Api.Domain
{
    public class Partido : Entity
    {
        protected Partido ()
        {
            
        }

        public Partido(
            string sigla,
            string nome)
            : this()
        {
            Sigla = sigla;
            Nome = nome;
        }
        public string Sigla { get; protected set; }
        public string Nome { get; protected set; }
        public PartidoSituacao Situacao { get; set; }
        public string UrlFoto { get; set; }

        private List<PartidoContato> _contatos;
        public IReadOnlyCollection<PartidoContato> Contatos { get => _contatos; }

        public PartidoContato AddContato(ContatoTipo tipo, string contato)
        {
            var c = new PartidoContato(this, tipo, contato);
            _contatos.Add(c);
            return c;
        }

    }
}