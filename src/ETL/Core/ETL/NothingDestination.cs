using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public class NothingDestination : IDestination
    {
        public async Task<StepValue<T>> Execute<T>(IStep<T> stepIn) where T : class
        {
            return await stepIn.Execute();
        }

        public async Task<IEnumerable<StepValue<T>>> Execute<T>(IEnumerable<StepValue<T>> stepIn) where T : class
        {
            return await stepIn.Execute();
        }

        public async Task<T> Execute<T>(StepValue<T> stepValue) where T : class
        {
            await Task.CompletedTask;
            return stepValue.Value;
        }
    }
}