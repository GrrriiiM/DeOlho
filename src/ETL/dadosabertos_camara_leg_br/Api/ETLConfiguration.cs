using System.Data;
using MySql.Data.MySqlClient;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api
{
    public class ETLConfiguration : IETLConfiguration
    {
        public string ConnectionString { get; set; }

        public string PartidoTableName { get; set; }
        public string PartidoURL { get; set; }
        public string PartidoDetailWithIdArgURL { get; set; }
        

        public string LegislaturaTableName { get; set; }
        public string LegislaturaURL { get; set; }

        public string DeputadoTableName { get; set; }
        public string DeputadoURL { get; set; }
        public string DeputadoDetailWithIdArgURL { get; set; }

        public string DespesaTableName { get; set; }
        public string DespesaDetailWithIdMonthYeahArgURL { get; set; }


        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}