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
using System.Data;
using Hangfire.Dashboard;
using Polly;
using System.Net.Http;
using Polly.Extensions.Http;
using RawRabbit.vNext;
using DeOlho.Scheduler.Dashboard.Jobs;

namespace DeOlho.Scheduler.Dashboard
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IETL_dadosabertos_camara_leg_br_Jobs, ETL_dadosabertos_camara_leg_br_Jobs>();

            services.AddHangfire(config => {
                config
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseColouredConsoleLogProvider()
                    .UseStorage(_configuration.GetSection("scheduler:configuration").Get<Configuration>().CreateStorage());
            });

            services.AddRawRabbit(
                config => config.AddConfiguration(_configuration.GetSection("RawRabbit:Configuration")),
                custom => {}
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHangfireDashboard("", new DashboardOptions
            {
                Authorization = new [] { new MyAuthorizationFilter() }
            });

            app.UseHangfireServer(new BackgroundJobServerOptions {
                WorkerCount = 1
            });

            RecurringJob.AddOrUpdate<IETL_dadosabertos_camara_leg_br_Jobs>(_ => _.ExecuteLegislatura(), () => Cron.Monthly());
            RecurringJob.AddOrUpdate<IETL_dadosabertos_camara_leg_br_Jobs>(_ => _.ExecutePartido(), () => Cron.Monthly());
            RecurringJob.AddOrUpdate<IETL_dadosabertos_camara_leg_br_Jobs>(_ => _.ExecuteDeputado(), () => Cron.Monthly());
            RecurringJob.AddOrUpdate<IETL_dadosabertos_camara_leg_br_Jobs>(_ => _.ExecuteDespesa(), () => Cron.Daily(4));
            RecurringJob.AddOrUpdate<IETL_dadosabertos_camara_leg_br_Jobs>(_ => _.ExecuteDespesaLastMonth(), () => Cron.Daily(5));
           
        }

        public class MyAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                return true;
            }
        }
    }
}
