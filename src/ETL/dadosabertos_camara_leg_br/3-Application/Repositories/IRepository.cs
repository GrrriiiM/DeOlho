using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_leg_br.Infrastructure.Data;

namespace DeOlho.ETL.dadosabertos_leg_br.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }

        T Add(T entity);
        void AddRange(IEnumerable<T> entities);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));
        T Find(object key);
        Task<T> FindAsync(object key, CancellationToken cancellationToken = default(CancellationToken));
        IEnumerable<T> ListAll();
        Task<IEnumerable<T>> ListAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        T Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
    }
}