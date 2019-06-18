using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_leg_br.Domain;

namespace DeOlho.ETL.dadosabertos_leg_br.Infrastructure.Repositories
{
    public interface IDeputadoFederalRepository : IRepository<DeputadoFederal>
    {
        Task<DeputadoFederal> FindByCPFAsync(long cpf);
        DeputadoFederal FindByCPF(long cpf);
    }
}