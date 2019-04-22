using System;
using System.Collections.Generic;
using System.Linq;
using DeOlho.ETL.Sources;
using DeOlho.ETL.Transforms;

namespace DeOlho.ETL
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Process()
                .Extract(() => new HttpStringSource("https://dadosabertos.camara.leg.br/api/v2/partidos", new { itens = 1000 }))
                .TransformJsonToDynamic()
                .TransformToList(_ => new List<dynamic>(_.dados))
                .Extract(_ => new HttpStringSource($"https://dadosabertos.camara.leg.br/api/v2/partidos/{_.id}"))
                .TransformJsonToDynamic()
                .Transform(_ => _.dados)
                .Execute()
                .Select(_ => _.nome)
                .ToList();

            Console.ReadLine();
            
        }
    }
}