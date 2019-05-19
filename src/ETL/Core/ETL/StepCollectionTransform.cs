using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public class StepCollectionTransform<TIn, TOut> : StepCollection<TOut>
    {
        readonly IStepCollection<TIn> _stepIn;
        readonly Func<StepValue<TIn>, Task<TOut>> _transform;

        public StepCollectionTransform(IStepCollection<TIn> stepIn, Func<StepValue<TIn>, Task<TOut>> _transform)
        {
            this._stepIn = stepIn;
            this._transform = _transform;
        }


        public async override Task<IEnumerable<StepValue<TOut>>> Execute()
        {
            var @in = await this._stepIn.Execute();
            var @out = new List<StepValue<TOut>>();
            foreach(var i in @in)
            {
                var value = await this._transform(i);
                @out.Add(new StepValue<TOut>(value, i));
            }
            return @out;
        }
    }
}