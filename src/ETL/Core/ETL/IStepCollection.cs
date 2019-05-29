using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public interface IStepCollection<T> : IEnumerable<StepValue<T>>
    {
        IStepCollection<TOut> Transform<TOut>(Func<StepValue<T>, TOut> transform) where TOut : class;
        IStepCollection<TOut> TransformAsync<TOut>(Func<StepValue<T>, Task<TOut>> transform) where TOut : class;
        IStepCollection<TOut> TransformToList<TOut>(Func<StepValue<T>, IEnumerable<TOut>> transform) where TOut : class;
        IStepCollection<TOut> Extract<TOut>(Func<StepValue<T>, ISource<TOut>> extract) where TOut : class;
        Task<IEnumerable<StepValue<T>>> Load(Func<IDestination> destination);
        Task<IEnumerable<StepValue<T>>> Load();
        Task<IEnumerable<StepValue<T>>> Execute(); 
    }
}