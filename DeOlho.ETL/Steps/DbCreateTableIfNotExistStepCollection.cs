using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dynamitey;
using Newtonsoft.Json.Linq;

namespace DeOlho.ETL.Steps
{
    public class DbCreateTableIfNotExistStepCollection<T> : StepCollection<T>
    {
        readonly string _tableName;
        readonly StepCollection<T> _stepIn;

        readonly IDbConnection _dbConnection;
        readonly IDbTransaction _dbTransaction;

        public DbCreateTableIfNotExistStepCollection(StepCollection<T> stepIn, IDbConnection dbConnection, IDbTransaction dbTransaction, string tableName)
        {
            this._tableName = tableName;
            this._stepIn = stepIn;
            this._dbConnection = dbConnection;
            this._dbTransaction = dbTransaction;
        }

        public async override Task<IEnumerable<T>> Execute()
        {
            var @in = await this._stepIn.Execute();
           
            var existTable = false;

            using (var command = this._dbConnection.CreateCommand())
            {
                command.Transaction = this._dbTransaction;
                command.CommandText = $"SELECT count(1) FROM information_schema.tables WHERE table_name = '{this._tableName}'";
                existTable = ((long)command.ExecuteScalar()) != 0;
            }
            
            if (!existTable)
            {
                var sqlColumns = new List<string>();

                var varcharTypes = new Type[] { typeof(string) };
                var intTypes = new Type[] { typeof(int), typeof(long), typeof(short) };
                var boolTypes = new Type[] { typeof(bool) };
                var dateTypes = new Type[] { typeof(DateTime) };
                var decimalTypes = new Type[] { typeof(decimal), typeof(double), typeof(float) };

                var propertyTypes = new Dictionary<string, Type>();

                
                propertyTypes = typeof(T).GetProperties().ToDictionary(_ => _.Name, _ => _.PropertyType);
                

                foreach(var property in propertyTypes)
                {
                    var isNullable = false;

                    var propertyType = property.Value;

                    if (propertyType.IsGenericType
                        && propertyType.GetGenericTypeDefinition() == typeof(Nullable<int>).GetGenericTypeDefinition())
                    {
                        propertyType = propertyType.GetGenericArguments()[0];
                        isNullable = true;
                    }

                    var columnsType = "";

                    if (varcharTypes.Contains(propertyType))
                    {
                        columnsType = "varchar(255)";
                        isNullable = true;
                    }
                    else if (intTypes.Contains(propertyType))
                    {
                        columnsType = "int";
                    }
                    else if (boolTypes.Contains(propertyType))
                    {
                        columnsType = "boolean";
                    }
                    else if (dateTypes.Contains(propertyType))
                    {
                        columnsType = "date";
                    }
                    else if (decimalTypes.Contains(propertyType))
                    {
                        columnsType = "decimal(24,8)";
                    }

                    if (columnsType != "")
                    {
                        sqlColumns.Add($"{property.Key} {columnsType} {(isNullable ? "" : "NOT NULL")}");
                    }
                    
                }

                if (sqlColumns.Any())
                {
                    var sql = $"CREATE TABLE {this._tableName} ({string.Join(",", sqlColumns)});";
                    
                    using (var command = this._dbConnection.CreateCommand())
                    {
                        command.Transaction = this._dbTransaction;
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                    }
                }
            }

            return @in;
        }
    }

    public static class DbCreateTableIfNotExistExtension
    {
        public static StepCollection<T> DbCreateTableIfNotExist<T>(this StepCollection<T> value, IDbConnection dbConnection, IDbTransaction dbTransaction, string tableName)
        {
            return new DbCreateTableIfNotExistStepCollection<T>(value, dbConnection, dbTransaction, tableName);
        }
    }

}