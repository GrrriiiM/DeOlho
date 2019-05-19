using System;
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
        readonly IStep<string> _stepIn;

        public JsonToDynamicTransform(IStep<string> stepIn)
            : base(stepIn, null)
        {
            this._stepIn = stepIn;
        }

        public async override Task<StepValue<dynamic>> Execute()
        {
            var @in = await this._stepIn.Execute();
            var @out = (dynamic)JValue.Parse(@in.Value).ToObject<dynamic>();
            return new StepValue<dynamic>(@out, @in);
        }
    }

    public class JsonToDynamicCollectionTransform : StepCollectionTransform<string, dynamic>
    {
        readonly IStepCollection<string> _stepIn;

        public JsonToDynamicCollectionTransform(IStepCollection<string> stepIn)
            : base(stepIn, null)
        {
            this._stepIn = stepIn;
        }

        public async override Task<IEnumerable<StepValue<dynamic>>> Execute()
        {
            var @in = await this._stepIn.Execute();
            var @out = new List<StepValue<dynamic>>(); 
            foreach(var i in @in)
            {
                var value = (dynamic)JValue.Parse(i.Value).ToObject<dynamic>(); 
                @out.Add(new StepValue<dynamic>(value, i));
            }
            return @out;
        }
    }

    public static class JsonToDynamicTransformExtensions
    {
        public static IStep<dynamic> TransformJsonToDynamic(this IStep<string> value)
        {
            return new JsonToDynamicTransform(value);
        }

        public static IStepCollection<dynamic> TransformJsonToDynamic(this IStepCollection<string> value)
        {
            return new JsonToDynamicCollectionTransform(value);
        }
    }
}