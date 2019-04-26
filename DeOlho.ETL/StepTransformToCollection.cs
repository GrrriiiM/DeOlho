using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public class StepTransformToCollection<TIn, TOut> : StepCollection<TOut>
    {
        readonly Step<TIn> _stepIn;
        readonly Func<TIn, IEnumerable<TOut>> _transform;

        public StepTransformToCollection(Step<TIn> stepIn, Func<TIn, IEnumerable<TOut>> _transform)
        {
            this._stepIn = stepIn;
            this._transform = _transform;
        }

        public async override Task<IEnumerable<TOut>> Execute()
        {
            var @in = await this._stepIn.Execute();
            var @out = this._transform(@in);
            return @out;
        }

    }
}