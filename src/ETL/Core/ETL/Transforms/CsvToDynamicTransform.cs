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
        readonly string _delimiter;
        public CsvToDynamicTransform(IStep<Stream> stepIn, string delimiter)
            : base(stepIn, null)
        {
            _delimiter = delimiter;
        }

        public async override Task<StepValue<IEnumerable<dynamic>>> Execute()
        {
            var @in = await this._stepIn.Execute();
            return @in.CsvToDynamic(_delimiter);
        }
    }

    public class CsvToDynamicCollectionTransform : StepCollectionTransform<Stream, IEnumerable<dynamic>>
    {
        readonly string _delimiter;

        public CsvToDynamicCollectionTransform(IStepCollection<Stream> stepIn, string delimiter)
            : base(stepIn, null)
        {
            _delimiter = delimiter;
        }

        public async override Task<IEnumerable<StepValue<IEnumerable<dynamic>>>> Execute()
        {
            var @in = await this._stepIn.Execute();
            var @out = new List<StepValue<IEnumerable<dynamic>>>(); 
            foreach(var i in @in)
            {
                @out.Add(i.CsvToDynamic(_delimiter));
            }
            return @out;
        }
    }



    public static class CsvToDynamicTransformExtensions
    {
        public static IStep<IEnumerable<dynamic>> TransformCsvToDynamic(this IStep<Stream> value, string delimiter = ",")
        {
            return new CsvToDynamicTransform(value, delimiter);
        }

        public static IStepCollection<IEnumerable<dynamic>> TransformCsvToDynamic(this IStepCollection<Stream> value, string delimiter = ",")
        {
            return new CsvToDynamicCollectionTransform(value, delimiter);
        }

        public static StepValue<IEnumerable<dynamic>> CsvToDynamic(this StepValue<Stream> value, string delimiter)
        {
            using (var sr = new StreamReader(value.Value))
            {
                using (var csv = new CsvReader(sr, new Configuration
                {
                    Delimiter = delimiter,
                    BadDataFound = _ => {
                        var a = _.ToString();
                    }
                }))
                {    
                    var records = csv.GetRecords<dynamic>().ToList();
                    return new StepValue<IEnumerable<dynamic>>(records, value);
                }
            }
        }
    }
}