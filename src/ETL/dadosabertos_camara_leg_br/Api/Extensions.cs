using System;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Infrastructure.Data;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Interfaces;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Services;
using DeOlho.EventBus.ELT.dadosabertos_camara_leg_br.Requests;
using DeOlho.EventBus.ELT.dadosabertos_camara_leg_br.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
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
                    var etlService = app.ApplicationServices.GetService<ILegislaturaService>();
                    await etlService.ExecuteETL();
                    return new LegislaturaResponse() {Message = $"Sucesso Legislatura: {req.Message}"};
                }
            );

            busClient.RespondAsync<PartidoRequest, PartidoResponse>(
                async (req, context) => {
                    var etlService = app.ApplicationServices.GetService<IPartidoService>();
                    await etlService.ExecuteETL();
                    return new PartidoResponse() {Message = $"Sucesso Partido: {req.Message}"};
                }
            );

            busClient.RespondAsync<DeputadoRequest, DeputadoResponse>(
                async (req, context) => {
                    var etlService = app.ApplicationServices.GetService<IPoliticoService>();
                    await etlService.ExecuteETL();
                    return new DeputadoResponse() {Message = $"Sucesso Deputado: {req.Message}"};
                }
            );

            busClient.RespondAsync<DespesaRequest, DespesaResponse>(
                async (req, context) => {
                    var etlService = app.ApplicationServices.GetService<IDespesaService>();
                    await etlService.ExecuteETL(req.Year, req.Month);
                    return new DespesaResponse() {Message = $"Sucesso Despesa: {req.Message}"};
                }
            );

            return app;
        }

        public static IHttpClientBuilder AddRetryPolicy(this IHttpClientBuilder value)
        {
            return value.AddPolicyHandler((_) =>
                HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    .WaitAndRetryAsync(new TimeSpan[] {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10)
                    }));
        }

        public static IServiceCollection AddServices(this IServiceCollection value)
        {
            value.AddHttpClient<ILegislaturaService, LegislaturaService>().AddRetryPolicy();
            value.AddHttpClient<IPartidoService, PartidoService>().AddRetryPolicy();
            value.AddHttpClient<IPoliticoService, PoliticoService>().AddRetryPolicy();
            value.AddHttpClient<IDespesaService, DespesaService>().AddRetryPolicy();
            return value;
        }

    }
}