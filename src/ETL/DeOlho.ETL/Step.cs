using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public abstract class Step<T> : IStep<T>
    {
        
        public IStep<TOut> Transform<TOut>(Func<T, Task<TOut>> transform)
        {
            return new StepTransform<T, TOut>(this, transform);
        }

        public IStepCollection<TOut> TransformToList<TOut>(Func<T, IEnumerable<TOut>> transform)
        {
            return new StepTransformToCollection<T, TOut>(this, transform);
        }

        public IStep<TOut> Extract<TOut>(Func<T, Source<TOut>> extract)
        {
            return new StepTransform<T, TOut>(this, async (_) => await extract(_).Execute());
        }

        public async Task<T> Load(Func<Destination> destination)
        {
            return await destination().Execute(this);
        }

        public abstract Task<T> Execute(); 

        public async Task<T> Load()
        {
            return (T)await this.Execute();
        }
    }
}