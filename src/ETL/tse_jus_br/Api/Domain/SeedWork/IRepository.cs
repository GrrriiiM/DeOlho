using System.Threading.Tasks;

namespace DeOlho.ETL.tse_jus_br.Api.Domain.SeedWork
{
    public interface IRepository<T> where T : Entity
    {
         Task<T> FindAsync(object key);
         T Add(T entity);
         T Update(T entity);
         T Remove(T entity);

    }
}