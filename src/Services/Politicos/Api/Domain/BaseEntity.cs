namespace DeOlho.Services.Politicos.Api.Domain
{
    public class BaseEntity
    {
        protected bool hasChange;
        protected BaseEntity() {}
        public long Id { get; protected set; }
        public long IntegrationTimestamp { get; protected set; }
        public string IntegrationOrigin { get; protected set; }

        public bool HasChange() => hasChange;
    }
}