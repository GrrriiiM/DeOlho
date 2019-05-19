using System;
using System.Data;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_camara_leg_br;
using MySql.Data.MySqlClient;

namespace DeOlho.Scheduler.Jobs
{
    public class ETL_dadosabertos_camara_leg_br_Jobs : IETL_dadosabertos_camara_leg_br_Jobs
    {

        public ETL_dadosabertos_camara_leg_br_Jobs()
        {
        }

        public void ExecuteLegislatura()
        {
            Task.WaitAll();
        }
        
    }
}