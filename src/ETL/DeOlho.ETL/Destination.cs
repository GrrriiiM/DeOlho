using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public abstract class Destination
    {
        public abstract Task<IEnumerable<T>> Execute<T>(StepCollection<T> stepIn);
        public abstract Task<T> Execute<T>(Step<T> stepIn);
    }
}