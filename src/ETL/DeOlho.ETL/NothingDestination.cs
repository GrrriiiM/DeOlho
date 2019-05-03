using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public class NothingDestination : IDestination
    {
        public async Task<IEnumerable<T>> Execute<T>(IStepCollection<T> stepIn)
        {
            return await stepIn.Execute();
        }

        public async Task<T> Execute<T>(IStep<T> stepIn)
        {
            return await stepIn.Execute();
        }
    }
}