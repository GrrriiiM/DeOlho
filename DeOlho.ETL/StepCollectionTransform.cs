using System;
using System.Collections.Generic;
using System.Linq;

namespace DeOlho.ETL
{
    public class StepCollectionTransform<TIn, TOut> : StepCollection<TOut>
    {
        readonly StepCollection<TIn> _stepIn;
        readonly Func<TIn, TOut> _transform;

        public StepCollectionTransform(StepCollection<TIn> stepIn, Func<TIn, TOut> _transform)
        {
            this._stepIn = stepIn;
            this._transform = _transform;
        }


        public override IEnumerable<TOut> Execute()
        {
            var @in = (IEnumerable<TIn>)this._stepIn.Execute();
            var @out = @in.Select(_ => this._transform(_)).ToList();
            return @out;
        }
    }
}