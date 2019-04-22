using System;

namespace DeOlho.ETL
{
    public abstract class Source<T>
    {
        public abstract T Execute();
    }
}