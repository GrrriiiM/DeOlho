using System;
using System.Collections.Generic;
using System.Linq;
using DeOlho.ETL.Destinations;
using DeOlho.ETL.Sources;
using DeOlho.ETL.Steps;
using DeOlho.ETL.Transforms;
using MySql.Data.MySqlClient;

namespace DeOlho.ETL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando");
            var mySqlConnection = new MySqlConnection("server=127.0.0.1;uid=root;pwd=mysql;database=deolhointegration;Allow User Variables=True");
            mySqlConnection.Open();
            try
            {
                var a = new Process()
                    .Extract(() => new HttpJsonSource("https://dadosabertos.camara.leg.br/api/v2/partidos", new { itens = 1000 }))
                    .TransformJsonToDynamic()
                    .TransformToList(_ => new List<dynamic>(_.dados))
                    .Extract(_ => new HttpJsonSource($"https://dadosabertos.camara.leg.br/api/v2/partidos/{_.id}"))
                    .TransformJsonToDynamic()
                    .Transform(_ => 
                        new {
                            Id = (long)_.dados.id,
                            Sigla = (string)_.dados.sigla,
                            Nome = (string)_.dados.nome,
                            Data = _.dados.status.data != null ? new Nullable<DateTime>(DateTime.Parse((string)_.dados.status.data)) : null,
                            LegislaturaId = (long)_.dados.status.idLegislatura,
                            Situacao = (string)_.dados.status.situacao,
                            TotalPosse = (int)_.dados.status.totalPosse,
                            TotalMembros = (int)_.dados.status.totalMembros,
                            LiderId = (string)_.dados.status.lider.uri,
                            UrlFacebook = (string)_.dados.urlFacebook,
                            UrlLogo = (string)_.dados.urlLogo,
                            UrlWebSite = (string)_.dados.urlWebSite
                        })
                    .MySqlCreateTableIfNotExist(mySqlConnection, "Partidos")
                    .Load(() => new MySqlDestination(mySqlConnection, "Partidos"));
                Console.WriteLine("Concluido");
            }
            catch (System.Exception)
            {
                
                throw;
            } 
            finally
            {
                mySqlConnection.Close();
            }

            Console.ReadLine();
            
        }
    }
}