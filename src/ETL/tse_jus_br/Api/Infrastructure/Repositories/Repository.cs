using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeOlho.ETL.tse_jus_br.Api.Domain.SeedWork;
using DeOlho.ETL.tse_jus_br.Api.Infrastructure.Data;

namespace DeOlho.ETL.tse_jus_br.Api.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {

        DeOlhoDbContext _deOlhoDbContext;
        protected readonly IQueryable<T> _entities;
        public Repository(
            DeOlhoDbContext deOlhoDbContext)
        {
            _deOlhoDbContext = deOlhoDbContext;
            _entities = _deOlhoDbContext.Set<T>();
        }

        public IUnitOfWork UnityOfWork { get => _deOlhoDbContext; }

        public T Add(T entity)
        {
            return _deOlhoDbContext.Add(entity).Entity;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _deOlhoDbContext.AddRange(entities);
        }

        public async Task<T> AddAsync(T entity)
        {
            return (await _deOlhoDbContext.AddAsync(entity)).Entity;
        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _deOlhoDbContext.AddRangeAsync(entities);
        }

        public T Find(object key)
        {
            return (T)_deOlhoDbContext.Find(typeof(T), key);
        }

        public async Task<T> FindAsync(object key)
        {
            return (T) await _deOlhoDbContext.FindAsync(typeof(T), key);
        }

        public void Remove(T entity)
        {
            _deOlhoDbContext.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _deOlhoDbContext.RemoveRange(entities);
        }

        public T Update(T entity)
        {
            return _deOlhoDbContext.Update(entity).Entity;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _deOlhoDbContext.UpdateRange(entities);
        }
    }
}