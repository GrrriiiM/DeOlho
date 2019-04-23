using System;
using System.Collections.Generic;

namespace DeOlho.ETL
{
    public abstract class StepCollection<T> : IStep<T>
    {

        public StepCollection<TOut> Transform<TOut>(Func<T, TOut> transform)
        {
            return new StepCollectionTransform<T, TOut>(this, transform);
        }

        public StepCollection<TOut> Extract<TOut>(Func<T, Source<TOut>> extract)
        {
            return new StepCollectionTransform<T, TOut>(this, (_) => {
                return extract(_).Execute();
            });
            
        }

        public IEnumerable<T> Load(Func<Destination> destination)
        {
            return destination().Execute(this);
        }

        public abstract IEnumerable<T> Execute(); 
    }
}