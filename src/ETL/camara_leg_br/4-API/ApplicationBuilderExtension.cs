using DeOlho.ETL.camara_leg_br.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeOlho.ETL.camara_leg_br.API
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
    }
}