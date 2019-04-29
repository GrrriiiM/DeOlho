using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DeOlho.ETL.Destinations
{
    public class DbDestination : Destination
    {
        readonly string _tableName;

        readonly IDbConnection _dbConnection;
        readonly IDbTransaction _dbTransaction;

        public DbDestination(IDbConnection dbConnection, IDbTransaction dbTransaction, string tableName)
        {
            this._tableName = tableName;
            this._dbConnection = dbConnection;
            this._dbTransaction = dbTransaction;
        }


        public async override Task<IEnumerable<T>> Execute<T>(StepCollection<T> stepIn)
        {
            var @in = await stepIn.Execute();
    
            var varcharTypes = new Type[] { typeof(string) };
            var intTypes = new Type[] { typeof(int), typeof(long), typeof(short) };
            var boolTypes = new Type[] { typeof(bool) };
            var dateTypes = new Type[] { typeof(DateTime) };
            var decimalTypes = new Type[] { typeof(decimal), typeof(double), typeof(float) };
            
            var propertyInfos = typeof(T).GetProperties();
            
            var sqlInserts = new List<string>();

            foreach(var item in @in)
            {
                var sqlColumnInserts = new List<string>();
                foreach(var propertyInfo in propertyInfos)
                {
                    var propertyType = propertyInfo.PropertyType;

                    if (propertyType.IsGenericType
                            && propertyType.GetGenericTypeDefinition() == typeof(Nullable<int>).GetGenericTypeDefinition())
                    {
                        propertyType = propertyType.GetGenericArguments()[0];
                    }

                    var value = propertyInfo.GetValue(item);

                    if (value == null)
                    {
                        sqlColumnInserts.Add("NULL");
                    }
                    else
                    {
                        if (varcharTypes.Contains(propertyType))
                        {
                            sqlColumnInserts.Add($"'{value.ToString().Replace('\'','`')}'");
                        }
                        else if (intTypes.Contains(propertyType))
                        {
                            sqlColumnInserts.Add(value.ToString());
                        }
                        else if (boolTypes.Contains(propertyType))
                        {
                            sqlColumnInserts.Add(((bool)value ? "TRUE" : "FALSE"));
                        }
                        else if (dateTypes.Contains(propertyType))
                        {
                            sqlColumnInserts.Add($"'{((DateTime)value).ToString("yyyy-MM-dd hh:mm:ss")}'");
                        }
                        else if (decimalTypes.Contains(propertyType))
                        {
                            sqlColumnInserts.Add(value.ToString());
                        }
                    }
                }

                sqlInserts.Add($"({string.Join(",", sqlColumnInserts)})");

            }

            if (sqlInserts.Any())
            {
                var sql = $"INSERT INTO {this._tableName} ({string.Join(",", propertyInfos.Select(_ => _.Name))}) VALUES {string.Join(",", sqlInserts)}";
                
                using (var command = this._dbConnection.CreateCommand())
                {
                    command.Transaction = this._dbTransaction;
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }

            return @in;
        }
        public async override Task<T> Execute<T>(Step<T> stepIn)
        {
            return await stepIn.Execute();
        }
    }
}