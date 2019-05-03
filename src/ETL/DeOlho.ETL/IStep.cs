using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public interface IStep<T>
    {
        IStep<TOut> Transform<TOut>(Func<T, TOut> transform);
        IStep<TOut> TransformAsync<TOut>(Func<T, Task<TOut>> transform);
        IStepCollection<TOut> TransformToList<TOut>(Func<T, IEnumerable<TOut>> transform);
        IStep<TOut> Extract<TOut>(Func<T, ISource<TOut>> extract);
        Task<T> Load(Func<IDestination> destination);
        Task<T> Execute(); 
        Task<T> Load();
    }
}