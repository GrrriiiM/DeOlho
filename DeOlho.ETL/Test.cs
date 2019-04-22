using System.Collections.Generic;
using DeOlho.ETL.Sources;
using DeOlho.ETL.Transforms;

namespace DeOlho.ETL
{
    public class Test
    {
        public Test()
        {
            var a= new Process();
            a.Extract(() => new HttpStringSource("https://dadosabertos.camara.leg.br/api/v2/partidos", new { itens = 1000 }))
                .TransformJsonToDynamic()
                .TransformToList(_ => (List<dynamic>)_.dados)
                .Extract(_ => new HttpStringSource($"https://dadosabertos.camara.leg.br/api/v2/partidos{_.id}"))
                .TransformJsonToDynamic()
                .Execute();
            // a.Extract(() => new HttpStringSource("https://dadosabertos.camara.leg.br/api/v2/partidos", new { itens = 1000 }))
            //     .TransformJsonToDynamic()
            //     .TransformToList(_ => new List<dynamic>(_.dados))
            //     .Extract(_ => new HttpStringSource($"https://dadosabertos.camara.leg.br/api/v2/partidos{_.id}"))
            //     .TransformJsonToDynamic()
            //     .Load();
                
                
        }
    }
}