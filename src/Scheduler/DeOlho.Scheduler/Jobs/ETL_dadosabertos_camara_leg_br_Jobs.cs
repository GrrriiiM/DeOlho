using System;
using System.Data;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_camara_leg_br;
using MySql.Data.MySqlClient;

namespace DeOlho.Scheduler.Jobs
{
    public class ETL_dadosabertos_camara_leg_br_Jobs : IETL_dadosabertos_camara_leg_br_Jobs
    {
        readonly IIntegrationService _integrationService;
        readonly IDbConnection _dbConnection;

        public ETL_dadosabertos_camara_leg_br_Jobs(
            IIntegrationService integrationService,
            IDbConnection dbConnection)
        {
            this._integrationService = integrationService;
            this._dbConnection = dbConnection;
        }

        private async Task execute(Func<IDbConnection, IDbTransaction, Task> execute)
        {

                IDbTransaction transaction = null;
                try
                {
                    this._dbConnection.Open();
                    transaction = this._dbConnection.BeginTransaction();
                    await execute(this._dbConnection, transaction);
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
                    if (this._dbConnection.State != ConnectionState.Closed)
                        this._dbConnection.Close();
                }
        }

        public void ExecutePartido()
        {
            Task.WaitAll(execute(async (dbConnection, dbTransaction) => await this._integrationService.ExecutePartido(dbConnection, dbTransaction)));
        }

        public void ExecuteLegislatura()
        {
            Task.WaitAll(execute(async (dbConnection, dbTransaction) => await this._integrationService.ExecuteLegislatura(dbConnection, dbTransaction)));
        }

        public void ExecuteDeputado()
        {
            Task.WaitAll(execute(async (dbConnection, dbTransaction) => await this._integrationService.ExecuteDeputado(dbConnection, dbTransaction)));
        }

        public void ExecuteDespesa()
        {
            Task.WaitAll(execute(async (dbConnection, dbTransaction) => await this._integrationService.ExecuteDespesa(dbConnection, dbTransaction)));
        }

        public void ExecuteDespesaLastMonth()
        {
            Task.WaitAll(execute(async (dbConnection, dbTransaction) => await this._integrationService.ExecuteDespesa(dbConnection, dbTransaction, DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month)));
        }
    }
}