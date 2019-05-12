using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeOlho.ETL.Transforms
{
    public class DescompressStreamTransform : StepTransform<Stream, Stream>
    {
        readonly IStep<Stream> _stepIn;

        readonly string _entryName;

        public DescompressStreamTransform(IStep<Stream> stepIn, string entryName = null)
            : base(stepIn, null)
        {
            this._stepIn = stepIn;

        }

        public async override Task<StepValue<Stream>> Execute()
        {
            var @in = await this._stepIn.Execute();
            return @in.DescompressStream(_entryName);
        }
    }

    public class DescompressStreamCollectionTransform : StepCollectionTransform<Stream, Stream>
    {
        readonly IStepCollection<Stream> _stepIn;
        readonly string _entryName;

        public DescompressStreamCollectionTransform(IStepCollection<Stream> stepIn, string entryName = null)
            : base(stepIn, null)
        {
            this._stepIn = stepIn;
            this._entryName = entryName;
        }

        public async override Task<IEnumerable<StepValue<Stream>>> Execute()
        {
            var @in = await this._stepIn.Execute();
            var @out = new List<StepValue<Stream>>(); 
            foreach(var i in @in)
            {
                @out.Add(i.DescompressStream(_entryName));
            }
            return @out;
        }
    }



    public static class DescompressStreamTransformExtensions
    {
        public static IStep<Stream> TransformDescompressStream(this IStep<Stream> value)
        {
            return new DescompressStreamTransform(value);
        }

        public static IStepCollection<Stream> TransformDescompressStream(this IStepCollection<Stream> value)
        {
            return new DescompressStreamCollectionTransform(value);
        }

        public static StepValue<Stream> DescompressStream(this StepValue<Stream> value, string entryName)
        {
            var zipArchive = new ZipArchive(value.Value);
            ZipArchiveEntry entry = null;
            if (!string.IsNullOrEmpty(entryName))
                entry = zipArchive.Entries.FirstOrDefault(_ => _.Name.ToUpper() == entryName.ToUpper());
            else
                entry = zipArchive.Entries.FirstOrDefault();

            return new StepValue<Stream>(entry.Open(), value);
        }
    }
}