using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public interface IStepCollection<T>
    {
        IStepCollection<TOut> Transform<TOut>(Func<T, TOut> transform);
        IStepCollection<TOut> TransformAsync<TOut>(Func<T, Task<TOut>> transform);
        IStepCollection<TOut> Extract<TOut>(Func<T, Source<TOut>> extract);
        Task<IEnumerable<T>> Load(Func<Destination> destination);
        Task<IEnumerable<T>> Execute(); 
    }
}