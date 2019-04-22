using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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

        public override dynamic Execute()
        {
            var @in = this._stepIn.Execute();
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

        public override IEnumerable<dynamic> Execute()
        {
            var @in = this._stepIn.Execute();
            var @out = @in.Select(_ => JValue.Parse(_).ToObject<dynamic>()).ToList();
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