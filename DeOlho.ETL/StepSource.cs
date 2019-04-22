using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DeOlho.ETL
{
    public class StepSource<TOut> : Step<TOut>
    {
        readonly Func<TOut> _source;

        public StepSource(Func<TOut> source)
        {
            this._source = source;
        }

        public override TOut Execute()
        {
            var @out = this._source();
            return @out;
        }
    }
}