using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Polly;
using Polly.Extensions.Http;
using RawRabbit.Configuration;
using RawRabbit.vNext;
using RawRabbit;
using Swashbuckle.AspNetCore.Swagger;
using System.Data.Common;
using DeOlho.EventBus.ELT.dadosabertos_camara_leg_br.Responses;
using DeOlho.EventBus.ELT.dadosabertos_camara_leg_br.Requests;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Interfaces;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api
{
    public class Startup
    {
        readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DeOlhoDbContext>(options => {
                options.UseMySql(_configuration.GetSection("ETL:Configuration:ConnectionString").Value);
            });

            services.AddServices();

            services.AddTransient<IETLConfiguration>(_ => _configuration.GetSection("ETL:Configuration").Get<ETLConfiguration>());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddRawRabbit(
                config => config.AddConfiguration(_configuration.GetSection("RawRabbit:Configuration")),
                custom => {}
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMigrate();

            app.UseMvc();
        }
    }
}
