using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public interface IDestination
    {
        Task<IEnumerable<StepValue<T>>> Execute<T>(IStepCollection<T> stepIn);
        Task<StepValue<T>> Execute<T>(IStep<T> stepIn);
    }
}