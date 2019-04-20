


using System;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.MySql;

namespace DeOlho.Scheduler
{
    public class Start
    {
        public Start(Configuration config)
        {
            GlobalConfiguration.Configuration
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseColouredConsoleLogProvider()
                .UseStorage(config.Storage);

            var jobs = new Jobs();


            RecurringJob.AddOrUpdate(() => jobs.MethodMinutely(), () => Cron.Minutely());

            var m1 = BackgroundJob.Enqueue(() => jobs.Method1());

            var m2 = BackgroundJob.ContinueJobWith(m1, () => jobs.Method2());
            var m3 = BackgroundJob.ContinueJobWith(m2, () => jobs.Method3());

            using (var server = new BackgroundJobServer())
            {
                config.Wait();
            }
        }

        

    }


    public class Jobs
    {
        public async Task Method1()
        {
            await Task.Delay(2000);
            System.Console.WriteLine("Metodo 1 Esperou 2 segundos");
        }

        public async Task Method2()
        {
            await Task.Delay(2000);
            System.Console.WriteLine("Metodo 2 Esperou 2 segundos");
        }
        public async Task Method3()
        {
            await Task.Delay(5000);
            System.Console.WriteLine("Metodo 2 Esperou 5 segundos");
        }

        public void MethodMinutely()
        {
            System.Console.WriteLine($"Metodo minuto {DateTime.Now}");
        }
    }
}