using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Domain;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Infrastructure.Data;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Interfaces;
using DeOlho.ETL.EFCore.Destinations;
using DeOlho.ETL.EFCore.Sources;
using DeOlho.ETL.Sources;
using DeOlho.ETL.Transforms;
using DeOlho.EventBus.Services.Politicos;
using DeOlho.EventBus.Services.Politicos.Messages;
using RawRabbit;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api.Services
{
    public class LegislaturaService : ILegislaturaService
    {

        readonly IETLConfiguration _configuration;
        readonly DeOlhoDbContext _deOlhoDbContext;
        readonly HttpClient _httpClient;
        readonly IBusClient _busClient;
        public LegislaturaService(
            HttpClient httpClient,
            DeOlhoDbContext deOlhoDbContext,
            IETLConfiguration configuration,
            IBusClient busClient)
        {
            _httpClient =  httpClient;
            _deOlhoDbContext = deOlhoDbContext;
            _configuration = configuration;
            _busClient = busClient;
        }


        public async Task ExecuteETL()
        {
            try
            {
                var load = await new Process()
                    .Extract(() => new HttpJsonSource(this._httpClient, this._configuration.LegislaturaURL))
                    .TransformJsonToDynamic()
                    .TransformToList(_ => new List<dynamic>(_.Value.dados))
                    .Transform(_ => 
                        new Legislatura {
                            Id = (long)_.Value.id,
                            DataInicio = (DateTime)_.Value.dataInicio,
                            DataFim = (DateTime)_.Value.dataFim
                        })
                    .Extract(_ => new DbContextSingleOrDefaultSource<Legislatura>(_deOlhoDbContext, _.Value.Id))
                    .Where(_ => compare(_.Value, (Legislatura)_.Parent.Value))
                    .Select(_ => (Legislatura)_.Parent.Value)
                    .ParallelForEach(_ => {
                        _busClient.PublishAsync(new LegislaturaMessage { Id = _.Id, DataInicio = _.DataInicio, DataFim = _.DataFim.Value });
                    })
                    .ToStepCollection()
                    .Load(() => new DbContextDestination(_deOlhoDbContext));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool compare(Legislatura o, Legislatura n)
        {
            return o == null || o.DataInicio != n.DataInicio || o.DataFim != n.DataFim;
        }
    }
}