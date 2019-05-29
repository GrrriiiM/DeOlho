using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public class NothingDestination : IDestination
    {
        public async Task<IEnumerable<StepValue<T>>> Execute<T>(IStepCollection<T> stepIn) where T : class
        {
            return await stepIn.Execute();
        }

        public async Task<StepValue<T>> Execute<T>(IStep<T> stepIn) where T : class
        {
            return await stepIn.Execute();
        }
    }
}