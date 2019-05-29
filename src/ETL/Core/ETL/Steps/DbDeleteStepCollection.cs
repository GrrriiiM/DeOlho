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
    public class DbDeleteStepCollection<T> : StepCollection<T> where T : class
    {
        readonly string _tableName;
        readonly IStepCollection<T> _stepIn;

        readonly IDbConnection _dbConnection;
        readonly IDbTransaction _dbTransaction;

        readonly string _whereCondition;

        public DbDeleteStepCollection(IStepCollection<T> stepIn, IDbConnection dbConnection, IDbTransaction dbTransaction, string tableName, string whereCondition = "")
        {
            this._tableName = tableName;
            this._stepIn = stepIn;
            this._dbConnection = dbConnection;
            this._dbTransaction = dbTransaction;
            this._whereCondition = whereCondition ?? "";
        }

        public async override Task<IEnumerable<StepValue<T>>> Execute()
        {
            var @in = await this._stepIn.Execute();

            using (var command = this._dbConnection.CreateCommand())
            {
                command.Transaction = this._dbTransaction;
                command.CommandText = $"DELETE FROM {this._tableName}{(_whereCondition.Any() ? " WHERE " : "")}{_whereCondition}";;
                command.ExecuteNonQuery();
            }

            return @in;
        }
    }

    public static class DbDeleteStepCollectionExtension
    {
        public static StepCollection<T> DbDelete<T>(this IStepCollection<T> value, IDbConnection dbConnection, IDbTransaction dbTransaction, string tableName, string whereCondition = "") where T : class
        {
            return new DbDeleteStepCollection<T>(value, dbConnection, dbTransaction, tableName, whereCondition);
        }
    }

}