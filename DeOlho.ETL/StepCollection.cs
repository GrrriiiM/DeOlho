using System;
using System.Collections.Generic;

namespace DeOlho.ETL
{
    public abstract class StepCollection<T> : IStep<T>
    {
        public IEnumerable<T> Itens { get; private set;}

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

        public StepCollection<T> Destination(Func<Destination<T>> destination)
        {
            return new StepCollectionTransform<T, T>(this, (_) => { destination().Execute(_); return _; });
        }

        public abstract IEnumerable<T> Execute(); 
    }
}