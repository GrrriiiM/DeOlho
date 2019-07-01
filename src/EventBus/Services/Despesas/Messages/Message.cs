using System;

namespace DeOlho.EventBus.Services.Despesas.Messages
{
    public class Message
    {
        public Message()
        {
            Timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            Origin = System.AppDomain.CurrentDomain.FriendlyName;
        }
        public string Id { get; private set; }
        public long Timestamp { get; private set; }
        public string Origin { get; private set; }
    }
}