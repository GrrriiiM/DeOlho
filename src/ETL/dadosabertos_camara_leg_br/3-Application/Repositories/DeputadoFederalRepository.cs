using System.Linq;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_leg_br.Domain;
using DeOlho.ETL.dadosabertos_leg_br.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.ETL.dadosabertos_leg_br.Infrastructure.Repositories
{
    public class DeputadoFederalRepository : Repository<DeputadoFederal>, IDeputadoFederalRepository
    {
        public DeputadoFederalRepository(
            DeOlhoDbContext deOlhoDbContext)
            : base(deOlhoDbContext)
        {
            
        }

        public async Task<DeputadoFederal> FindByCPFAsync(long cpf)
        {
            return await Query.SingleOrDefaultAsync(_ => _.CPF == cpf);
        }

        public DeputadoFederal FindByCPF(long cpf)
        {
            return Query.SingleOrDefault(_ => _.CPF == cpf);
        }
    }
}