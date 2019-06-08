using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public interface IDestination
    {
        Task<T> Execute<T>(StepValue<T> stepValue) where T : class;
        Task<IEnumerable<StepValue<T>>> Execute<T>(IEnumerable<StepValue<T>> stepIn) where T : class;
        Task<StepValue<T>> Execute<T>(IStep<T> stepIn) where T : class;
    }
}