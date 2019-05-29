using System;
using System.Threading.Tasks;
using DeOlho.EventBus.ELT.dadosabertos_camara_leg_br.Requests;
using DeOlho.EventBus.ELT.dadosabertos_camara_leg_br.Responses;
using RawRabbit;

namespace DeOlho.Scheduler.Dashboard.Jobs
{
    public class ETL_dadosabertos_camara_leg_br_Jobs : IETL_dadosabertos_camara_leg_br_Jobs
    {

        readonly IBusClient _busClient;
        public ETL_dadosabertos_camara_leg_br_Jobs(
            IBusClient busClient)
        {
            _busClient = busClient;
        }

        public void ExecuteLegislatura()
        {
            Task.WaitAll(executeLegislatura());
        }
        private async Task executeLegislatura()
        {
            var response = await _busClient.RequestAsync<LegislaturaRequest, LegislaturaResponse>(new LegislaturaRequest { Message ="Requisição Hangfire" });
            Console.WriteLine(response.Message);
        }
        
        public void ExecutePartido()
        {
            executePartido().Wait(-1);
        }
        private async Task executePartido()
        {
            var response = await _busClient.RequestAsync<PartidoRequest, PartidoResponse>(new PartidoRequest { Message ="Requisição Hangfire" });
            Console.WriteLine(response.Message);
        }

        public void ExecuteDeputado()
        {
            executeDeputado().Wait(-1);
        }
        private async Task executeDeputado()
        {
            var response = await _busClient.RequestAsync<DeputadoRequest, DeputadoResponse>(new DeputadoRequest { Message ="Requisição Hangfire" });
            Console.WriteLine(response.Message);
        }

        public void ExecuteDespesa()
        {
            executeDespesa().Wait();
        }
        private async Task executeDespesa()
        {
            var response = await _busClient.RequestAsync<DespesaRequest, DespesaResponse>(
                new DespesaRequest { 
                    Year = DateTime.Now.Year,
                    Month = DateTime.Now.Month,
                    Message ="Requisição Hangfire" 
                });
            Console.WriteLine(response.Message);
        }

        public void ExecuteDespesaLastMonth()
        {
            executeDespesaLastMonth().Wait();
        }
        private async Task executeDespesaLastMonth()
        {
            var response = await _busClient.RequestAsync<DespesaRequest, DespesaResponse>(
                new DespesaRequest { 
                    Year = DateTime.Now.Year,
                    Month = DateTime.Now.AddMonths(-1).Month,
                    Message ="Requisição Hangfire" 
                });
            Console.WriteLine(response.Message);
        }
    }
}