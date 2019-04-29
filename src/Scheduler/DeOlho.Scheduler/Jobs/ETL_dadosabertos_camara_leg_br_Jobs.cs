using System;
using System.Data;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_camara_leg_br;
using MySql.Data.MySqlClient;

namespace DeOlho.Scheduler.Jobs
{
    public class ETL_dadosabertos_camara_leg_br_Jobs
    {
        private static Configuration.ETL_dadosabertos_camara_leg_br_Configuration configuration;

        public static void SetConfiguration(Configuration.ETL_dadosabertos_camara_leg_br_Configuration configuration)
        {
            ETL_dadosabertos_camara_leg_br_Jobs.configuration = configuration;
        }


        private async Task execute(Func<IDbConnection, IDbTransaction, Task> execute)
        {
            using(IDbConnection connection = new MySqlConnection(configuration.DestinationConnectionString))
            {
                IDbTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    await execute(connection, transaction);
                    transaction.Commit();
                    transaction = null;
                }
                catch (Exception)
                {
                    
                    throw;
                }
                finally
                {
                    if (transaction != null)
                        transaction.Rollback();
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }
        }

        public void ExecutePartido()
        {
            Task.WaitAll(execute(async (dbConnection, dbTransaction) => await new IntegrationService(new System.Net.Http.HttpClient(), configuration).ExecutePartido(dbConnection, dbTransaction)));
        }

        public void ExecuteLegislatura()
        {
            Task.WaitAll(execute(async (dbConnection, dbTransaction) => await new IntegrationService(new System.Net.Http.HttpClient(), configuration).ExecuteLegislatura(dbConnection, dbTransaction)));
        }

        public void ExecuteDeputado()
        {
            Task.WaitAll(execute(async (dbConnection, dbTransaction) => await new IntegrationService(new System.Net.Http.HttpClient(), configuration).ExecuteDeputado(dbConnection, dbTransaction)));
        }
    }
}