namespace DeOlho.Services.Politicos.Api.Domain
{
    public class Entity
    {
        protected Entity() {}
        public long Id { get; protected set; }
        public long IntegrationTimestamp { get; protected set; }
        public string IntegrationOrigin { get; protected set; }
        public string IntegrationId { get; protected set; }

        public void SetIntegration(
            long integrationTimestamp, 
            string integrationOrigin,
            string integrationId)
        {
            IntegrationTimestamp = integrationTimestamp;
            IntegrationOrigin = integrationOrigin;
            IntegrationId = integrationId;
        }
    }
}