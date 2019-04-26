using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public class StepCollectionTransform<TIn, TOut> : StepCollection<TOut>
    {
        readonly StepCollection<TIn> _stepIn;
        readonly Func<TIn, Task<TOut>> _transform;

        public StepCollectionTransform(StepCollection<TIn> stepIn, Func<TIn, Task<TOut>> _transform)
        {
            this._stepIn = stepIn;
            this._transform = _transform;
        }


        public async override Task<IEnumerable<TOut>> Execute()
        {
            var @in = await this._stepIn.Execute();
            var @out = new List<TOut>();
            foreach(var i in @in)
            {
                @out.Add(await this._transform(i));
            }
            return @out;
        }
    }
}