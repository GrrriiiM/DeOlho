using System.Threading.Tasks;
using DeOlho.ETL.tse_jus_br.Api.Domain.SeedWork;

namespace DeOlho.ETL.tse_jus_br.Api.Domain
{
    public interface IPoliticoRepository : IRepository<Politico>
    {
         Task<Politico> FindByCPFAsync(long cpf);
         
    }
}