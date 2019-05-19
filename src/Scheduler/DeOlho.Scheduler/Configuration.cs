using System;
using Hangfire;
using Hangfire.MySql;
using Hangfire.MySql.Core;

namespace DeOlho.Scheduler
{
    public class Configuration
    {
        private string connectionString;
        private JobStorage storage; 

        public string ConnectionString { get => connectionString; 
            set 
            {
                Console.WriteLine($"connection:{connectionString}");
                Console.WriteLine($"connection:{value}");
                connectionString = value;
                storage = new MySqlStorage(connectionString, new MySqlStorageOptions {
                        
                });
            }  
        }
        public JobStorage Storage  { get => storage; }
    }
}