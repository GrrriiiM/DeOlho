using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public abstract class StepCollection<T> : IStepCollection<T> where T : class
    {

        public IStepCollection<TOut> Transform<TOut>(Func<StepValue<T>, TOut> transform) where TOut : class
        {
            return new StepCollectionTransform<T, TOut>(this, async (_) =>
            { 
                return await Task.Run<TOut>(() => transform(_));
            });
        }

        public IStepCollection<TOut> TransformAsync<TOut>(Func<StepValue<T>, Task<TOut>> transform) where TOut : class
        {
            return new StepCollectionTransform<T, TOut>(this, transform);
        }

        public IStepCollection<TOut> TransformToList<TOut>(Func<StepValue<T>, IEnumerable<TOut>> transform) where TOut : class
        {
            return new StepCollectionTransformToCollection<T, TOut>(this, transform);    
        }

        public IStepCollection<TOut> Extract<TOut>(Func<StepValue<T>, ISource<TOut>> extract) where TOut : class
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

    public class _StepCollection<T> : StepCollection<T> where T : class
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