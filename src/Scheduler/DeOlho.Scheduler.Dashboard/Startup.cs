using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Hangfire;
using DeOlho.Scheduler.Jobs;
using DeOlho.ETL.dadosabertos_camara_leg_br;
using System.Data;
using MySql.Data.MySqlClient;

namespace DeOlho.Scheduler.Dashboard
{
    public class Startup
    {
        readonly Configuration _schedulerConfiguration;
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            _schedulerConfiguration = configuration.GetSection("scheduler:configuration").Get<Configuration>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddHttpClient();
            services.AddHttpClient<IIntegrationService, IntegrationService>();
            services.AddTransient<IIntegrationServiceConfiguration>(_ => _schedulerConfiguration.ETL_dadosabertos_camara_leg_br);
            services.AddTransient<IDbConnection>(_ => new MySqlConnection(_schedulerConfiguration.ETL_dadosabertos_camara_leg_br.DestinationConnectionString));
            services.AddScoped<IETL_dadosabertos_camara_leg_br_Jobs, ETL_dadosabertos_camara_leg_br_Jobs>();

            // var type = typeof(ITypedHttpClientFactory<>).Assembly.DefinedTypes.Single(t => t.Name.Contains("DefaultTypedHttpClientFactory"));
            // services.AddTransient(typeof(ITypedHttpClientFactory<>), type);

            services.AddHangfire(config => {
                config
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseColouredConsoleLogProvider()
                    .UseStorage(_schedulerConfiguration.Storage);
            });

            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            
            // app.UseHangfireServer(storage: _schedulerConfiguration.Storage);
            app.UseHangfireDashboard("");//, storage: _schedulerConfiguration.Storage);
            //app.UseHangfireServer();

            RecurringJob.AddOrUpdate<IETL_dadosabertos_camara_leg_br_Jobs>(_ => _.ExecutePartido(), () => Cron.Monthly());
            RecurringJob.AddOrUpdate<IETL_dadosabertos_camara_leg_br_Jobs>(_ => _.ExecuteLegislatura(), () => Cron.Monthly());
            RecurringJob.AddOrUpdate<IETL_dadosabertos_camara_leg_br_Jobs>(_ => _.ExecuteDeputado(), () => Cron.Monthly());
            RecurringJob.AddOrUpdate<IETL_dadosabertos_camara_leg_br_Jobs>(_ => _.ExecuteDespesa(), () => Cron.Daily(2));
            RecurringJob.AddOrUpdate<IETL_dadosabertos_camara_leg_br_Jobs>(_ => _.ExecuteDespesaLastMonth(), () => Cron.Daily(3));
            // app.Run(async (context) =>
            // {
            //     await context.Response.WriteAsync("Hello World!");
            // });

            //new Start(_schedulerConfiguration);
        }
    }
}
