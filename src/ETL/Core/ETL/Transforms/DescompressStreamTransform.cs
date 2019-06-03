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
    public class DescompressStreamTransform : StepTransform<Stream, List<StreamDescompressed>>
    {
        readonly IStep<Stream> _stepIn;

        public DescompressStreamTransform(IStep<Stream> stepIn)
            : base(stepIn, null)
        {
            this._stepIn = stepIn;

        }

        public async override Task<StepValue<List<StreamDescompressed>>> Execute()
        {
            var @in = await this._stepIn.Execute();
            return @in.DescompressStream();
        }
    }

    public class DescompressStreamCollectionTransform : StepCollectionTransform<Stream, List<StreamDescompressed>>
    {
        readonly IStepCollection<Stream> _stepIn;

        public DescompressStreamCollectionTransform(IStepCollection<Stream> stepIn)
            : base(stepIn, null)
        {
            this._stepIn = stepIn;
        }

        public async override Task<IEnumerable<StepValue<List<StreamDescompressed>>>> Execute()
        {
            var @in = await this._stepIn.Execute();
            var @out = new List<StepValue<List<StreamDescompressed>>>(); 
            foreach(var i in @in)
            {
                @out.Add(i.DescompressStream());
            }
            return @out;
        }
    }



    public static class DescompressStreamTransformExtensions
    {
        public static IStep<List<StreamDescompressed>> TransformDescompressStream(this IStep<Stream> value)
        {
            return new DescompressStreamTransform(value);
        }

        public static IStepCollection<List<StreamDescompressed>> TransformDescompressStream(this IStepCollection<Stream> value)
        {
            return new DescompressStreamCollectionTransform(value);
        }

        public static StepValue<List<StreamDescompressed>> DescompressStream(this StepValue<Stream> value)
        {
            var zipArchive = new ZipArchive(value.Value);
            var streams = new List<StreamDescompressed>();


            foreach(var entry in zipArchive.Entries)
            {
                streams.Add(new StreamDescompressed
                {
                    Name = entry.Name,
                    Stream = entry.Open()
                });
            }

            return new StepValue<List<StreamDescompressed>>(streams, value);
        }
    }

    public class StreamDescompressed
    {
        public string Name { get; set; }
        public Stream Stream { get; set; }
        
    }
}