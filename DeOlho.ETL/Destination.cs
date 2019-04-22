using System;

namespace DeOlho.ETL
{
    public class Destination<T>
    {
        readonly Action<T> _destination;
        public Destination(Action<T> destination)
        {
            _destination = destination;
        }

        public void Execute(T @in)
        {
            _destination(@in);
        }
    }
}