using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public interface IStepCollection<T>
    {
        IStepCollection<TOut> Transform<TOut>(Func<StepValue<T>, TOut> transform);
        IStepCollection<TOut> TransformAsync<TOut>(Func<StepValue<T>, Task<TOut>> transform);
        IStepCollection<TOut> TransformToList<TOut>(Func<StepValue<T>, IEnumerable<TOut>> transform);
        IStepCollection<TOut> Extract<TOut>(Func<StepValue<T>, ISource<TOut>> extract);
        Task<IEnumerable<StepValue<T>>> Load(Func<IDestination> destination);
        Task<IEnumerable<StepValue<T>>> Load();
        Task<IEnumerable<StepValue<T>>> Execute(); 
    }
}