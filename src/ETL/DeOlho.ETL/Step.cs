using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public abstract class Step<T> : IStep<T>
    {

        public IStep<TOut> Transform<TOut>(Func<StepValue<T>, TOut> transform)
        {
            return new StepTransform<T, TOut>(this, async (_) =>
            { 
                return await Task.Run<TOut>(() => transform(_));
            });
        }

        public IStep<TOut> TransformAsync<TOut>(Func<StepValue<T>, Task<TOut>> transform)
        {
            return new StepTransform<T, TOut>(this, transform);
        }



        public IStepCollection<TOut> TransformToList<TOut>(Func<StepValue<T>, IEnumerable<TOut>> transform)
        {
            return new StepTransformToCollection<T, TOut>(this, transform);
        }

        public IStep<TOut> Extract<TOut>(Func<StepValue<T>, ISource<TOut>> extract)
        {
            return new StepTransform<T, TOut>(this, async (_) => await extract(_).Execute());
        }

        public async Task<StepValue<T>> Load(Func<IDestination> destination)
        {
            return await destination().Execute(this);
        }
        public async Task<StepValue<T>> Load()
        {
            return await this.Load(() => new NothingDestination());
        }

        public abstract Task<StepValue<T>> Execute(); 
        
    }
}