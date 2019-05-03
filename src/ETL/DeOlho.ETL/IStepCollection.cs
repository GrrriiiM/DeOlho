using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public interface IStepCollection<T>
    {
        IStepCollection<TOut> Transform<TOut>(Func<T, TOut> transform);
        IStepCollection<TOut> TransformAsync<TOut>(Func<T, Task<TOut>> transform);
        IStepCollection<TOut> Extract<TOut>(Func<T, ISource<TOut>> extract);
        Task<IEnumerable<T>> Load(Func<IDestination> destination);
        Task<IEnumerable<T>> Load();
        Task<IEnumerable<T>> Execute(); 
    }
}