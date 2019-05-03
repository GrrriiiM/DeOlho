using System;

namespace DeOlho.ETL
{
    public class Process
    {

        public Step<T> Extract<T>(Func<ISource<T>> source)
        {
            return new StepSource<T>(async () => await source().Execute());
        }
    }

}