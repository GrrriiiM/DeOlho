using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public static class Extensions
    {

        public static IEnumerable<T> ParallelForEach<T>(this IEnumerable<T> value, Action<T> action, int maxDegreeOfParallelism = 4)
        {
            Parallel.ForEach(
                value, 
                new ParallelOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism }, 
                action);
            return value; 
        }

        public static IEnumerable<StepValue<T>> ForEach<T>(this IEnumerable<StepValue<T>> value, Action<StepValue<T>> action)
        {
            Func<StepValue<T>, StepValue<T>> func = _ => { action(_); return _; };
            foreach(var item in value)
            {
                yield return func(item);
            }
        }

        public static IEnumerable<StepValue<TOut>> Select<TIn, TOut>(this IEnumerable<StepValue<TIn>> value, Func<StepValue<TIn>, TOut> selector) where TIn : class where TOut : class
        {
            return Enumerable.Select(value, _ => new StepValue<TOut>(selector(_), _));
        }

        public static IEnumerable<StepValue<T>> Where<T>(this IEnumerable<StepValue<T>> value, Func<StepValue<T>, bool> predicate) where T : class
        {
            return Enumerable.Where(value, predicate);
        }

        public static IEnumerable<StepValue<TOut>> TransformToList<TIn, TOut>(this IEnumerable<StepValue<TIn>> value, Func<StepValue<TIn>, IEnumerable<TOut>> transform) where TOut : class
        {
            return Enumerable.SelectMany(value, _ => transform(_).Select(_1 => new StepValue<TOut>(_1, _)));
        }

        public async static Task<IEnumerable<StepValue<T>>> Load<T>(this IEnumerable<StepValue<T>> value, Func<IDestination> destination) where T : class
        {
            return await value.Load(destination);
        }

        public async static Task<IEnumerable<StepValue<T>>> Execute<T>(this IEnumerable<StepValue<T>> value) where T : class
        {
            var task = new Task<IEnumerable<StepValue<T>>>(() => value.ToList());
            return await task;
        }
    }
}