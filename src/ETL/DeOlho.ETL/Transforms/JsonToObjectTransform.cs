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

        public dynamic Dynamic { get; private set;}

        public JsonToDynamicTransform(IStep<string> stepIn)
            : base(stepIn, null)
        {
            this._stepIn = stepIn;
        }

        public async override Task<dynamic> Execute()
        {
            var @in = await this._stepIn.Execute();
            var @out = (dynamic)JValue.Parse(@in).ToObject<dynamic>();
            return @out;
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

        public async override Task<IEnumerable<dynamic>> Execute()
        {
            var @in = await this._stepIn.Execute();
            var @out = new List<dynamic>(); 
            foreach(var i in @in)
            {
                @out.Add((dynamic)JValue.Parse(i).ToObject<dynamic>());
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