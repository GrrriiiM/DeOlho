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
                connectionString = value;
                storage = new MySqlStorage(connectionString, new MySqlStorageOptions {
                        
                });
            }  
        }
        public Action Wait { get; set; }

        
        public JobStorage Storage  { get => storage; }
    }
}