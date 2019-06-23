using System.Threading;
using System.Threading.Tasks;

namespace DeOlho.Services.Despesas.Infrastucture.Data
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
    }
}