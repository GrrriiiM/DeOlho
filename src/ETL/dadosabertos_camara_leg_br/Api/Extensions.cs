using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Infra.Data;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Services;
using DeOlho.EventBus.ELT.dadosabertos_camara_leg_br.Requests;
using DeOlho.EventBus.ELT.dadosabertos_camara_leg_br.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigrate(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var deOlhoDbContext = serviceScope.ServiceProvider.GetService<DeOlhoDbContext>())
                {
                    deOlhoDbContext.Database.Migrate();
                }
            }

            return app;
        }

        public static IApplicationBuilder UseEventBus(this IApplicationBuilder app)
        {
            var busClient = app.ApplicationServices.GetService<IBusClient>();

            busClient.RespondAsync<LegislaturaRequest, LegislaturaResponse>(
                async (req, context) => {
                    var etlService = app.ApplicationServices.GetService<IETLService>();
                    await etlService.ExecuteLegislatura();
                    return new LegislaturaResponse() {Message = $"Sucesso Legislatura: {req.Message}"};
                }
            );

            busClient.RespondAsync<PartidoRequest, PartidoResponse>(
                async (req, context) => {
                    var etlService = app.ApplicationServices.GetService<IETLService>();
                    await etlService.ExecutePartido();
                    return new PartidoResponse() {Message = $"Sucesso Partido: {req.Message}"};
                }
            );

            busClient.RespondAsync<DeputadoRequest, DeputadoResponse>(
                async (req, context) => {
                    var etlService = app.ApplicationServices.GetService<IETLService>();
                    await etlService.ExecuteDeputado();
                    return new DeputadoResponse() {Message = $"Sucesso Deputado: {req.Message}"};
                }
            );

            busClient.RespondAsync<DespesaRequest, DespesaResponse>(
                async (req, context) => {
                    var etlService = app.ApplicationServices.GetService<IETLService>();
                    await etlService.ExecuteDespesa(req.Year, req.Month);
                    return new DespesaResponse() {Message = $"Sucesso Despesa: {req.Message}"};
                }
            );

            return app;
        }
    }
}