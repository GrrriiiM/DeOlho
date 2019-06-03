using Microsoft.AspNetCore.Builder;
using RawRabbit;
using Microsoft.Extensions.DependencyInjection;
using DeOlho.EventBus.Services.Politicos;
using DeOlho.Services.Politicos.Api.Infrastructure.Data;
using DeOlho.Services.Politicos.Api.Domain;
using Microsoft.EntityFrameworkCore;
using DeOlho.EventBus.Services.Politicos.Messages;
using DeOlho.Services.Politicos.Api.IntegrationEvents.Subscribes;

namespace Deolho.Services.Politicos.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPoliticoChangeSubscribe, PoliticoChangeSubscribe>();
            return services;
        }

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

            busClient.SubscribeAsync<PoliticoChangeMessage>(
                async (msg, context) => {
                    using(var scope = services.CreateScope())
                    {
                        var service = scope.ServiceProvider.GetService<IPoliticoChangeSubscribe>();
                        await service.Execute(msg);
                    }
                }
            );

            return app;
        }
    }
}