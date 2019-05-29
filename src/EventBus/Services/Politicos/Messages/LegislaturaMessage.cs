using System;

namespace DeOlho.EventBus.Services.Politicos
{
    public class LegislaturaMessage : BaseMessage
    {
        public long Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get;  set; }
    }
}