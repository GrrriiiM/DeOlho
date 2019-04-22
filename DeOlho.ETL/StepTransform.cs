using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DeOlho.ETL
{
    public class StepTransform<TIn, TOut> : Step<TOut>
    {
        readonly Step<TIn> _stepIn;
        readonly Func<TIn, TOut> _transform;

        public StepTransform(Step<TIn> stepIn, Func<TIn, TOut> transform)
        {
            this._stepIn = stepIn;
            this._transform = transform;
        }

        public override TOut Execute()
        {
            var @in = this._stepIn.Execute();
            var @out = this._transform(@in);
            return @out;
        }
    }
}