using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Dynamitey;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace DeOlho.ETL.Steps
{
    public class MySqlCreateTableIfNotExistStepCollection<T> : StepCollection<T>
    {
        readonly string _tableName;
        readonly StepCollection<T> _stepIn;

        readonly MySqlConnection _mySqlConnection;

        public MySqlCreateTableIfNotExistStepCollection(StepCollection<T> stepIn, MySqlConnection mySqlConnection, string tableName)
        {
            this._tableName = tableName;
            this._stepIn = stepIn;
            this._mySqlConnection = mySqlConnection;
        }

        public override IEnumerable<T> Execute()
        {
            var @in = this._stepIn.Execute();
           
            var item = @in.FirstOrDefault();
            if (item != null)
            {
                var existTable = false;

                using (var command = this._mySqlConnection.CreateCommand())
                {
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

                    // if (item is IDynamicMetaObjectProvider)
                    // {
                    //     foreach(var propertyName in Dynamic.GetMemberNames(@in.FirstOrDefault(), true))
                    //     {
                    //         var value = Dynamic.InvokeGet(item, propertyName);
                    //         if (value is JValue)
                    //         {
                    //             var type = (JTokenType)((JValue)value).Type;
                    //             if (type == JTokenType.String) propertyTypes.Add(propertyName, typeof(string));
                    //             if (type == JTokenType.Integer) propertyTypes.Add(propertyName, typeof(int));
                    //             if (type == JTokenType.Boolean) propertyTypes.Add(propertyName, typeof(bool));
                    //             if (type == JTokenType.Date) propertyTypes.Add(propertyName, typeof(DateTime?));
                    //             if (type == JTokenType.Float) propertyTypes.Add(propertyName, typeof(decimal));
                    //         }
                    //         else
                    //         {
                    //             propertyTypes.Add(propertyName, value.GetType());
                    //         }
                            
                    //     }
                    // }
                    // else
                    // {
                        propertyTypes = typeof(T).GetProperties().ToDictionary(_ => _.Name, _ => _.PropertyType);
                    //}

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
                        var sql = $"CREATE TABLE {this._tableName} ({string.Join(',', sqlColumns)});";
                        
                        using (var command = this._mySqlConnection.CreateCommand())
                        {
                            command.CommandText = sql;
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }

            return @in;
        }
    }

    public static class MySqlCreateTableIfNotExistExtension
    {
        public static StepCollection<T> MySqlCreateTableIfNotExist<T>(this StepCollection<T> value, MySqlConnection mySqlConnection, string tableName)
        {
            return new MySqlCreateTableIfNotExistStepCollection<T>(value, mySqlConnection, tableName);
        }
    }

}