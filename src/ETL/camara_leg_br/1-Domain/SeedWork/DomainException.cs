using System;
using System.Runtime.Serialization;

namespace DeOlho.ETL.camara_leg_br.Domain.SeedWork
{
    public class DomainException : Exception
    {
        public DomainException() : base() {}

        public DomainException(string message) : base(message) {}
        public DomainException(string message, Exception innerException) : base(message, innerException) {}
        public DomainException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}