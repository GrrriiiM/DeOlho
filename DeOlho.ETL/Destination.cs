using System;
using System.Collections.Generic;

namespace DeOlho.ETL
{
    public abstract class Destination
    {
        public abstract IEnumerable<T> Execute<T>(StepCollection<T> stepIn);
        public abstract T Execute<T>(Step<T> stepIn);
    }
}