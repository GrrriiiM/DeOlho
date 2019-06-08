using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeOlho.ETL.tse_jus_br.Api.Domain.SeedWork
{
    public interface IRepository<T> where T : Entity
    {
        
        IUnitOfWork UnityOfWork { get; }
        Task<T> FindAsync(object key);
        T Find(object key);
        void AddRange(IEnumerable<T> entities);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        T Add(T entity);
        void UpdateRange(IEnumerable<T> entities);
        T Update(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Remove(T entity);

    }
}