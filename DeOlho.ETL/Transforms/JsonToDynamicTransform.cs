using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeOlho.ETL.Transforms
{
    public class JsonToDynamicTransform : StepTransform<string, dynamic>
    {
        readonly Step<string> _stepIn;

        public JsonToDynamicTransform(Step<string> stepIn)
            : base(stepIn, null)
        {
            this._stepIn = stepIn;
        }

        public async override Task<dynamic> Execute()
        {
            var @in = await this._stepIn.Execute();
            var @out = JValue.Parse(@in).ToObject<dynamic>();
            return @out;
        }
    }

    public class JsonToDynamicCollectionTransform : StepCollectionTransform<string, dynamic>
    {
        readonly StepCollection<string> _stepIn;

        public JsonToDynamicCollectionTransform(StepCollection<string> stepIn)
            : base(stepIn, null)
        {
            this._stepIn = stepIn;
        }

        public async override Task<IEnumerable<dynamic>> Execute()
        {
            var @in = await this._stepIn.Execute();
            var @out = new List<dynamic>(); 
            foreach(var i in @in)
            {
                @out.Add(JValue.Parse(i).ToObject<dynamic>());
            }
            return @out;
        }
    }

    public static class JsonToDynamicTransformExtensions
    {
        public static Step<dynamic> TransformJsonToDynamic(this Step<string> value)
        {
            return new JsonToDynamicTransform(value);
        }

        public static StepCollection<dynamic> TransformJsonToDynamic(this StepCollection<string> value)
        {
            return new JsonToDynamicCollectionTransform(value);
        }
    }
}