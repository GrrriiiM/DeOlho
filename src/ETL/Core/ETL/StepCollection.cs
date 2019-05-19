using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public abstract class StepCollection<T> : IStepCollection<T>
    {

        public IStepCollection<TOut> Transform<TOut>(Func<StepValue<T>, TOut> transform)
        {
            return new StepCollectionTransform<T, TOut>(this, async (_) =>
            { 
                return await Task.Run<TOut>(() => transform(_));
            });
        }

        public IStepCollection<TOut> TransformAsync<TOut>(Func<StepValue<T>, Task<TOut>> transform)
        {
            return new StepCollectionTransform<T, TOut>(this, transform);
        }

        public IStepCollection<TOut> TransformToList<TOut>(Func<StepValue<T>, IEnumerable<TOut>> transform)
        {
            return new StepCollectionTransformToCollection<T, TOut>(this, transform);    
        }

        public IStepCollection<TOut> Extract<TOut>(Func<StepValue<T>, ISource<TOut>> extract)
        {
            return new StepCollectionTransform<T, TOut>(this, async (_) => {
                return await extract(_).Execute();
            });
            
        }

        public async Task<IEnumerable<StepValue<T>>> Load(Func<IDestination> destination)
        {
            return await destination().Execute(this);
        }

        public async Task<IEnumerable<StepValue<T>>> Load()
        {
            return await this.Load(() => new NothingDestination());
        }

        public abstract Task<IEnumerable<StepValue<T>>> Execute();

        public IEnumerator<StepValue<T>> GetEnumerator()
        {
            return this.Execute().Result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class StepCollectionExtensions
    {
        public static IStepCollection<T> ToStepCollection<T>(this IEnumerable<StepValue<T>> value)
        {
            return new StepCollectionTransform<T, T>(new _StepCollection<T>(value), async (_) => await Task.Run(() => _.Value)); 
        }
    }

    public class _StepCollection<T> : StepCollection<T>
    {
        readonly IEnumerable<StepValue<T>> _values;
        public _StepCollection(IEnumerable<StepValue<T>> values)
        {
            _values = values;
        }

        public async override Task<IEnumerable<StepValue<T>>> Execute()
        {
            return await Task.Run(() => _values);
        }
    } 
}