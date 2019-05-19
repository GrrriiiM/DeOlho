using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeOlho.ETL.Transforms
{
    public class CsvToDynamicTransform : StepTransform<Stream, IEnumerable<dynamic>>
    {
        readonly IStep<Stream> _stepIn;

        public CsvToDynamicTransform(IStep<Stream> stepIn)
            : base(stepIn, null)
        {
        }

        public async override Task<StepValue<IEnumerable<dynamic>>> Execute()
        {
            var @in = await this._stepIn.Execute();
            return @in.CsvToDynamic();
        }
    }

    public class CsvToDynamicCollectionTransform : StepCollectionTransform<Stream, IEnumerable<dynamic>>
    {
        readonly IStepCollection<Stream> _stepIn;

        public CsvToDynamicCollectionTransform(IStepCollection<Stream> stepIn)
            : base(stepIn, null)
        {
            this._stepIn = stepIn;
        }

        public async override Task<IEnumerable<StepValue<IEnumerable<dynamic>>>> Execute()
        {
            var @in = await this._stepIn.Execute();
            var @out = new List<StepValue<IEnumerable<dynamic>>>(); 
            foreach(var i in @in)
            {
                @out.Add(i.CsvToDynamic());
            }
            return @out;
        }
    }



    public static class CsvToDynamicTransformExtensions
    {
        public static IStep<IEnumerable<dynamic>> TransformCsvToDynamic(this IStep<Stream> value)
        {
            return new CsvToDynamicTransform(value);
        }

        public static IStepCollection<IEnumerable<dynamic>> TransformCsvToDynamic(this IStepCollection<Stream> value)
        {
            return new CsvToDynamicCollectionTransform(value);
        }

        public static StepValue<IEnumerable<dynamic>> CsvToDynamic(this StepValue<Stream> value)
        {
            using (var sr = new StreamReader(value.Value))
            {
                using (var csv = new CsvReader(sr, new Configuration
                {
                    Delimiter = ","
                }))
                {    
                    var records = csv.GetRecords<dynamic>().ToList();
                    return new StepValue<IEnumerable<dynamic>>(records, value);
                }
            }
        }
    }
}