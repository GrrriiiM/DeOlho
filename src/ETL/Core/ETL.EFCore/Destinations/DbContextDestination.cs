using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeOlho.ETL;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.ETL.EFCore.Destinations
{
    public class DbContextDestination : IDestination
    {
        readonly DbContext _dbContext; 

        public DbContextDestination(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<StepValue<T>>> Execute<T>(IEnumerable<StepValue<T>> stepIn) where T : class
        {
            var @in = await stepIn.Execute();

            foreach(var stepValue in @in)
            {
                _dbContext.Set<T>().Add(stepValue.Value);    
            }

            await _dbContext.SaveChangesAsync();
            return @in;
        }

        public async Task<StepValue<T>> Execute<T>(IStep<T> stepIn) where T : class
        {
            var @in = await stepIn.Execute();
            _dbContext.Set<T>().Add(@in.Value);
            _dbContext.SaveChanges();
            return @in;
        }
    }
}