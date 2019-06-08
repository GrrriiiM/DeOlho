using System.Threading.Tasks;

namespace DeOlho.ETL.tse_jus_br.Api.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }
}