using System;
using System.Threading.Tasks;

namespace DeOlho.ETL
{
    public abstract class Source<T> : ISource<T>
    {
        public abstract Task<T> Execute();
    }
}