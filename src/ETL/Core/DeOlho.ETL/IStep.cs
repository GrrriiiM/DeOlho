using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public interface IStep<T>
    {
        IStep<TOut> Transform<TOut>(Func<StepValue<T>, TOut> transform);
        IStep<TOut> TransformAsync<TOut>(Func<StepValue<T>, Task<TOut>> transform);
        IStepCollection<TOut> TransformToList<TOut>(Func<StepValue<T>, IEnumerable<TOut>> transform);
        IStep<TOut> Extract<TOut>(Func<StepValue<T>, ISource<TOut>> extract);
        Task<StepValue<T>> Load(Func<IDestination> destination);
        Task<StepValue<T>> Load();
        Task<StepValue<T>> Execute(); 
        
    }
}