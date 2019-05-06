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
        public ETL_dadosabertos_camara_leg_br_Configuration ETL_dadosabertos_camara_leg_br { get; set; }

        public class ETL_dadosabertos_camara_leg_br_Configuration : ETL.dadosabertos_camara_leg_br.IIntegrationServiceConfiguration
        {
            public string DestinationConnectionString { get; set; }
            public string PartidoURL { get; set; }
            public string PartidoDetailWithIdArgURL { get; set; }
            public string PartidoTableName { get; set; }
            public string LegislaturaTableName { get; set; }
            public string LegislaturaURL { get; set; }

            public string DeputadoURL { get; set; }
            public string DeputadoDetailWithIdArgURL { get; set; }
            public string DeputadoTableName { get; set; }

            public string DespesaDetailWithIdMonthYeahArgURL { get; set; }
            public string DespesaTableName { get; set; }
        }
    }
}