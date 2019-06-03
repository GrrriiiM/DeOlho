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
    public class CsvTransform<T> : StepTransform<Stream, IEnumerable<T>>
    {
        readonly string _delimiter;
        public CsvTransform(IStep<Stream> stepIn, string delimiter)
            : base(stepIn, null)
        {
            _delimiter = delimiter;
        }

        public async override Task<StepValue<IEnumerable<T>>> Execute()
        {
            var @in = await _stepIn.Execute();
            return @in.CsvTo<T>(_delimiter);
        }
    }

    public class CsvCollectionTransform<T> : StepCollectionTransform<Stream, IEnumerable<T>>
    {
        readonly string _delimiter;

        public CsvCollectionTransform(IStepCollection<Stream> stepIn, string delimiter)
            : base(stepIn, null)
        {
            _delimiter = delimiter;
        }

        public async override Task<IEnumerable<StepValue<IEnumerable<T>>>> Execute()
        {
            var @in = await _stepIn.Execute();
            var @out = new List<StepValue<IEnumerable<T>>>(); 
            foreach(var i in @in)
            {
                @out.Add(i.CsvTo<T>(_delimiter));
            }
            return @out;
        }
    }



    public static class CsvTransformExtensions
    {
        public static IStep<IEnumerable<T>> TransformCsv<T>(this IStep<Stream> value, string delimiter = ",")
        {
            return new CsvTransform<T>(value, delimiter);
        }

        public static IStepCollection<IEnumerable<T>> TransformCsv<T>(this IStepCollection<Stream> value, string delimiter = ",")
        {
            return new CsvCollectionTransform<T>(value, delimiter);
        }

        public static StepValue<IEnumerable<T>> CsvTo<T>(this StepValue<Stream> value, string delimiter)
        {
            using (var sr = new StreamReader(value.Value))
            {
                using (var csv = new CsvReader(sr, new Configuration
                {
                    Delimiter = delimiter
                }))
                {    
                    var records = csv.GetRecords<T>().ToList();
                    return new StepValue<IEnumerable<T>>(records, value);
                }
            }
        }
    }
}