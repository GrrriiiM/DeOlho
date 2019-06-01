using System;
using System.Reflection;

namespace DeOlho.EventBus.Services.Politicos
{
    public class BaseMessage
    {
        public BaseMessage()
        {
            Timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            Origin = System.AppDomain.CurrentDomain.FriendlyName;
        }
        public string Id { get; private set; }
        public long Timestamp { get; private set; }
        public string Origin { get; private set; }
    }
}