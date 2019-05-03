using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DeOlho.ETL.Destinations
{
    public class DbDestinationCollection : IDestination
    {
        readonly string _tableName;

        readonly IDbConnection _dbConnection;
        readonly IDbTransaction _dbTransaction;

        public DbDestinationCollection(IDbConnection dbConnection, IDbTransaction dbTransaction, string tableName)
        {
            this._tableName = tableName;
            this._dbConnection = dbConnection;
            this._dbTransaction = dbTransaction;
        }


        public async Task<IEnumerable<T>> Execute<T>(IStepCollection<T> stepIn)
        {
            var @in = await stepIn.Execute();

            var sqlInserts = new List<string>();

            foreach(var item in @in)
            {
                var propertyInfos = item.GetType().GetProperties();

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
                        if (Constants.Db.VarcharTypes.Contains(propertyType))
                        {
                            sqlColumnInserts.Add($"'{value.ToString().Replace('\'','`')}'");
                        }
                        else if (Constants.Db.IntTypes.Contains(propertyType))
                        {
                            sqlColumnInserts.Add(value.ToString());
                        }
                        else if (Constants.Db.BoolTypes.Contains(propertyType))
                        {
                            sqlColumnInserts.Add(((bool)value ? "TRUE" : "FALSE"));
                        }
                        else if (Constants.Db.DateTypes.Contains(propertyType))
                        {
                            sqlColumnInserts.Add($"'{((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss")}'");
                        }
                        else if (Constants.Db.DecimalTypes.Contains(propertyType))
                        {
                            sqlColumnInserts.Add(String.Format(CultureInfo.InvariantCulture, "{0}", value));
                        }
                    }
                }

                sqlInserts.Add($"({string.Join(",", sqlColumnInserts)})");

            }

            if (sqlInserts.Any())
            {
                var propertyInfos = @in.FirstOrDefault().GetType().GetProperties();

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
        public async Task<T> Execute<T>(IStep<T> stepIn)
        {
            throw new NotImplementedException();
        }

    }
}