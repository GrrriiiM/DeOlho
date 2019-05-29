using System;

namespace DeOlho.Services.Politicos.Api.Domain
{
    public partial class Legislatura : BaseEntity
    {
        public Legislatura(
            long id,
            DateTime dataInicio,
            DateTime dataFim,
            long integrationTimestamp, 
            string integrationOrigin)
        {
            Id = id;
            this.Update(dataInicio, dataFim, integrationTimestamp, integrationOrigin);
        }

        public DateTime DataInicio { get; protected set; }
        public DateTime DataFim { get; protected set; }

        
    }
}