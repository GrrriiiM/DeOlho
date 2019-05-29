using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public static class Extensions
    {

        public static IStepCollection<T> ToStepCollection<T>(this IEnumerable<StepValue<T>> value) where T : class
        {
            return new StepCollectionTransform<T, T>(new _StepCollection<T>(value), async (_) => await Task.Run(() => _.Value)); 
        }

        public static IStepCollection<T> ToStepCollection<T>(this IEnumerable<T> value) where T : class
        {
            var stepValues = value.Select(_ => new StepValue<T>(_, null));
            return new StepCollectionTransform<T, T>(new _StepCollection<T>(stepValues), async (_) => await Task.Run(() => _.Value)); 
        }

        public static IEnumerable<T> ParallelForEach<T>(this IEnumerable<T> value, Action<T> action, int maxDegreeOfParallelism = 4)
        {
            Parallel.ForEach(
                value, 
                new ParallelOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism }, 
                action);
            return value; 
        }
    }
}