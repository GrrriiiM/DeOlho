// using Microsoft.Extensions.Configuration;
// using System;
// using System.IO;
// using Hangfire;

// namespace DeOlho.Scheduler.Console
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             var builder = new ConfigurationBuilder()
//                 .SetBasePath(Directory.GetCurrentDirectory())
//                 .AddJsonFile("config.json", optional: false, reloadOnChange: true);

//             IConfigurationRoot configuration = builder.Build();
//             var schedulerConfiguration = configuration.GetSection("scheduler:configuration").Get<Configuration>();

//             schedulerConfiguration.Wait = () => {
//                 while(System.Console.ReadLine().ToUpper() != "EXIT");
//             };

//             new Start(schedulerConfiguration);
            
//         }


//     }
// }
