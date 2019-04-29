using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public abstract class StepCollection<T> : IStepCollection<T>
    {

        public IStepCollection<TOut> Transform<TOut>(Func<T, TOut> transform)
        {
            return new StepCollectionTransform<T, TOut>(this, async (_) =>
            { 
                return await Task.Run<TOut>(() => transform(_));
            });
        }

        public IStepCollection<TOut> TransformAsync<TOut>(Func<T, Task<TOut>> transform)
        {
            return new StepCollectionTransform<T, TOut>(this, transform);
        }

        public IStepCollection<TOut> Extract<TOut>(Func<T, Source<TOut>> extract)
        {
            return new StepCollectionTransform<T, TOut>(this, async (_) => {
                return await extract(_).Execute();
            });
            
        }

        public async Task<IEnumerable<T>> Load(Func<Destination> destination)
        {
            return await destination().Execute(this);
        }

        public abstract Task<IEnumerable<T>> Execute();

    }
}