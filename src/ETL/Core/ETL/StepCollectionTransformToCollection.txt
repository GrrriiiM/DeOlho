using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public class StepCollectionTransformToCollection<TIn, TOut> : StepCollection<TOut> where TOut : class
    {
        readonly IStepCollection<TIn> _stepIn;
        readonly Func<StepValue<TIn>, IEnumerable<TOut>> _transform;

        public StepCollectionTransformToCollection(IStepCollection<TIn> stepIn, Func<StepValue<TIn>, IEnumerable<TOut>> _transform)
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
                var value = this._transform(i);
                @out.AddRange(value.Select(_ => new StepValue<TOut>(_, i)));
            }
            return @out;
        }

    }
}