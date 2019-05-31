using System;

namespace DeOlho.EventBus.Services.Politicos.Messages
{
    public class PartidoMessage : BaseMessage
    {
        public long Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public DateTime? Data { get; set; }
        public long LegislaturaId { get; set; }
        public string Situacao { get; set; }
        public int TotalPosse { get; set; }
        public int TotalMembros { get; set; }
        public string LiderId { get; set; }
        public string UrlFacebook { get; set; }
        public string UrlLogo { get; set; }
        public string UrlWebSite { get; set; }
    }
}