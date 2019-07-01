using DeOlho.EventBus.Services.Despesas.Messages;
using DeOlho.Services.Despesas.Application.Commands;
using DeOlho.Services.Despesas.Infrastucture.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;

namespace DeOlho.Services.Despesas.API
{
    public static class ApplicationBuilderExtension
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
            var services = app.ApplicationServices;

            var busClient = services.GetService<IBusClient>();
            
            busClient.SubscribeAsync<DespesaCreatedMessage>(
                async (msg, context) => {
                    using(var scope = services.CreateScope())
                    {
                        var mediator = (IMediator)scope.ServiceProvider.GetService(typeof(IMediator));
                        var createDespesaCommand = new CreateDespesasCommand
                        {
                            CPF = msg.CPF,
                            TipoId = msg.TipoId,
                            Ano = msg.Ano,
                            Mes = msg.Mes,
                            Valor = msg.Valor
                        };
                        await mediator.Send(createDespesaCommand);
                    }
                }
            );

            return app;
        }
    }
}