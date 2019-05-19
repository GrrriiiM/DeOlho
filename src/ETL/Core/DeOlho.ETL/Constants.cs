using System;

namespace DeOlho.ETL
{
    public static class Constants
    {
        public static class Db
        {
            public static Type[] VarcharTypes = new Type[] { typeof(string) };
            public static Type[] IntTypes = new Type[] { typeof(int), typeof(long), typeof(short) };
            public static Type[] BoolTypes = new Type[] { typeof(bool) };
            public static Type[] DateTypes = new Type[] { typeof(DateTime) };
            public static Type[] DecimalTypes = new Type[] { typeof(decimal), typeof(double), typeof(float) };
        }
    }
}