using System.Collections.Generic;

namespace DeOlho.EventBus.Services.Politicos.Messages
{
    public class PartidoChangeMessage : BaseMessage
    {
        public string Sigla { get; protected set; }
        public string Nome { get; protected set; }
        public int Situacao { get; set; }
        public string UrlFoto { get; set; }
        public List<Contato> Contatos { get; set; } = new List<Contato>();
        public class Contato
        {
            public int Tipo { get; set; }
            public string Valor { get; set; }
        }
    }
}