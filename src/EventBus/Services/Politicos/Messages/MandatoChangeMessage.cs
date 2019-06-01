using System;

namespace DeOlho.EventBus.Services.Politicos.Messages
{
    public class MandatoChangeMessage : BaseMessage
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int Tipo { get; set; }
    }
}