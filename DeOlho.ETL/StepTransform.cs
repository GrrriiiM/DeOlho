using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public class StepTransform<TIn, TOut> : Step<TOut>
    {
        readonly Step<TIn> _stepIn;
        readonly Func<TIn, Task<TOut>> _transform;

        public StepTransform(Step<TIn> stepIn, Func<TIn, Task<TOut>> transform)
        {
            this._stepIn = stepIn;
            this._transform = transform;
        }

        public async override Task<TOut> Execute()
        {
            var @in = await this._stepIn.Execute();
            var @out = await this._transform(@in);
            return @out;
        }
    }
}