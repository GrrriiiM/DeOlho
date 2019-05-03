using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public interface IDestination
    {
        Task<IEnumerable<T>> Execute<T>(IStepCollection<T> stepIn);
        Task<T> Execute<T>(IStep<T> stepIn);
    }
}