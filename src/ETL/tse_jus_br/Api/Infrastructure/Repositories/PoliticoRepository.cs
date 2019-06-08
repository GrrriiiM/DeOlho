using System.Linq;
using System.Threading.Tasks;
using DeOlho.ETL.tse_jus_br.Api.Domain;
using DeOlho.ETL.tse_jus_br.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.ETL.tse_jus_br.Api.Infrastructure.Repositories
{
    public class PoliticoRepository : Repository<Politico>, IPoliticoRepository
    {
        public PoliticoRepository(
            DeOlhoDbContext deOlhoDbContext)
            : base(deOlhoDbContext)
        {
            
        }

        public async Task<Politico> FindByCPFAsync(long cpf)
        {
            return await _entities.SingleOrDefaultAsync(_ => _.NR_CPF_CANDIDATO == cpf);
        }
    }
}