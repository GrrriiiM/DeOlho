using Microsoft.AspNetCore.Builder;
using RawRabbit;
using Microsoft.Extensions.DependencyInjection;
using DeOlho.EventBus.Services.Politicos;
using DeOlho.Services.Politicos.Api.Infrastructure.Data;
using DeOlho.Services.Politicos.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Deolho.Services.Politicos.Api
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
            var services = app.ApplicationServices;

            var busClient = services.GetService<IBusClient>();

            busClient.SubscribeAsync<LegislaturaMessage>(
                async (msg, context) => {
                    using(var scope = services.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetService<DeOlhoDbContext>();
                        var legislatura = await dbContext.Legislaturas.FindAsync(msg.Id);
                        if (legislatura == null)
                        {
                            legislatura = new Legislatura(msg.Id, msg.DataInicio, msg.DataFim, msg.Timestamp, msg.Origin);
                            await dbContext.AddAsync(legislatura);
                        }
                        else
                        {
                            legislatura.Update(msg.DataInicio, msg.DataFim, msg.Timestamp, msg.Origin);
                            dbContext.Update(legislatura);
                        }
                        await dbContext.SaveChangesAsync();
                    }
                }
            );

            return app;
        }
    }
}