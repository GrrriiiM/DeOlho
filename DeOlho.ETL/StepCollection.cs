using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public abstract class StepCollection<T> : IStep<T>
    {

        public StepCollection<TOut> Transform<TOut>(Func<T, TOut> transform)
        {
            return new StepCollectionTransform<T, TOut>(this, async (_) =>
            { 
                return await Task.Run<TOut>(() => transform(_));
            });
        }

        public StepCollection<TOut> TransformAsync<TOut>(Func<T, Task<TOut>> transform)
        {
            return new StepCollectionTransform<T, TOut>(this, transform);
        }

        public StepCollection<TOut> Extract<TOut>(Func<T, Source<TOut>> extract)
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