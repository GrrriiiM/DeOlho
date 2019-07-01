using System;
using DeOlho.ETL.camara_leg_br.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace DeOlho.ETL.camara_leg_br.API.Extensions
{
    public static class ServiceCollectionExtension
    {
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

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var healthCheckBuilder = services.AddHealthChecks();
            var configurationRabbitMQ = configuration.GetSection("RawRabbit:Configuration");
            var configurationRabbitMQUserName = configurationRabbitMQ.GetValue<string>("Username");
            var configurationRabbitMQPassword = configurationRabbitMQ.GetValue<string>("Password");
            var configurationRabbitMQPort = configurationRabbitMQ.GetValue<string>("Port");
            var configurationRabbitMQHostname = configurationRabbitMQ.GetValue<string>("Hostnames:0");  
            var rabbitMQConnectionString = $"amqp://{configurationRabbitMQUserName}:{configurationRabbitMQPassword}@{configurationRabbitMQHostname}:{configurationRabbitMQPort}/";
            healthCheckBuilder.AddRabbitMQ(rabbitMQConnectionString, name: "rabbitmq");

            healthCheckBuilder.AddMySql(configuration.GetConnectionString("ETL"), "mysql");


            var etlConfiguration = configuration.GetSection("ETL:Configuration").Get<ETLConfiguration>();
            
            var deputadoFederalNotaFiscalUrl = etlConfiguration.DeputadoFederalNotaFiscalUrl;
            deputadoFederalNotaFiscalUrl = deputadoFederalNotaFiscalUrl.Substring(0, deputadoFederalNotaFiscalUrl.IndexOf("?"));
            deputadoFederalNotaFiscalUrl = $"{deputadoFederalNotaFiscalUrl}?itens=1";
            deputadoFederalNotaFiscalUrl = string.Format(deputadoFederalNotaFiscalUrl, 204510);
            healthCheckBuilder.AddUrlGroup(new Uri(deputadoFederalNotaFiscalUrl), name: deputadoFederalNotaFiscalUrl);

            var deputadoFederalDetailUrl = etlConfiguration.DeputadoFederalDetailUrl;
            deputadoFederalDetailUrl = string.Format(deputadoFederalDetailUrl, 204510);
            healthCheckBuilder.AddUrlGroup(new Uri(deputadoFederalDetailUrl), name: deputadoFederalDetailUrl);

            var deputadoFederalUrl = etlConfiguration.DeputadoFederalUrl;
            deputadoFederalUrl = deputadoFederalUrl.Substring(0, deputadoFederalUrl.IndexOf("?"));
            deputadoFederalUrl = $"{deputadoFederalUrl}?itens=1";
            deputadoFederalUrl = string.Format(deputadoFederalUrl, 204510);
            healthCheckBuilder.AddUrlGroup(new Uri(deputadoFederalUrl), name: deputadoFederalUrl);

            return services;
        }
    }
}