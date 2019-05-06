


// using System;
// using System.Threading.Tasks;
// using DeOlho.Scheduler.Jobs;
// using Hangfire;
// using Hangfire.MySql;

// namespace DeOlho.Scheduler
// {
//     public class Start
//     {

//         public Start(Configuration config)
//         {
//             GlobalConfiguration.Configuration
//                 .UseSimpleAssemblyNameTypeSerializer()
//                 .UseRecommendedSerializerSettings()
//                 .UseColouredConsoleLogProvider()
//                 .UseStorage(config.Storage);


//             ETL_dadosabertos_camara_leg_br_Jobs.SetConfiguration(config.ETL_dadosabertos_camara_leg_br);
            
//             RecurringJob.AddOrUpdate<ETL_dadosabertos_camara_leg_br_Jobs>(_ => _.ExecutePartido(), () => Cron.Monthly());

//             RecurringJob.AddOrUpdate<ETL_dadosabertos_camara_leg_br_Jobs>(_ => _.ExecuteLegislatura(), () => Cron.Monthly());

//             RecurringJob.AddOrUpdate<ETL_dadosabertos_camara_leg_br_Jobs>(_ => _.ExecuteDeputado(), () => Cron.Monthly());

//             new ETL_dadosabertos_camara_leg_br_Jobs().ExecutePartido();

//             using (var server = new BackgroundJobServer())
//             {
//                 config.Wait();
//             }
//         }

        

//     }

// }