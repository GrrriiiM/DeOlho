using System;
using System.Collections.Generic;

namespace DeOlho.ETL
{
    public abstract class Step<T> : IStep<T>
    {
        
        public Step<TOut> Transform<TOut>(Func<T, TOut> transform)
        {
            return new StepTransform<T, TOut>(this, transform);
        }

        public StepCollection<TOut> TransformToList<TOut>(Func<T, IEnumerable<TOut>> transform)
        {
            return new StepTransformToCollection<T, TOut>(this, transform);
        }

        public Step<TOut> Extract<TOut>(Func<T, Source<TOut>> extract)
        {
            return new StepTransform<T, TOut>(this, (_) => extract(_).Execute());
        }

        public Step<T> Destination(Func<Destination<T>> destination)
        {
            return new StepTransform<T, T>(this, (_) => { destination().Execute(_); return _; });
        }

        public abstract T Execute(); 

        public T Load()
        {
            return (T)this.Execute();
        }
    }
}