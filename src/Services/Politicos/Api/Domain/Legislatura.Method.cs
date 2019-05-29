using System;

namespace DeOlho.Services.Politicos.Api.Domain
{
    public partial class Legislatura
    {
        
        public void Update(
            DateTime dataInicio,
            DateTime dataFim,
            long integrationTimestamp, 
            string integrationOrigin)
        {
            if(DataInicio != dataInicio)
            {
                DataInicio = dataInicio;
                hasChange = true;
            }

            if(DataFim != dataFim)
            {
                DataFim = dataFim;
                hasChange = true;
            }

            if(IntegrationTimestamp != integrationTimestamp)
            {
                IntegrationTimestamp = integrationTimestamp;
                hasChange = true;
            }

            if(DataInicio != dataInicio)
            {
                DataInicio = dataInicio;
                hasChange = true;
            }
        }

    }
}