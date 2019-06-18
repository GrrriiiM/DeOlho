using System.Linq;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_leg_br.Domain;
using DeOlho.ETL.dadosabertos_leg_br.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DeOlho.ETL.dadosabertos_leg_br.Infrastructure.Repositories
{
    public class NotaFiscalRepository : Repository<NotaFiscal>, INotaFiscalRepository
    {
        public NotaFiscalRepository(DeOlhoDbContext deOlhoDbContext)
         : base(deOlhoDbContext)
         {}

        public NotaFiscal FindByCodigoDocumento(long codigoDocumento)
        {
            return Query.SingleOrDefault(_ => _.CodDocumento == codigoDocumento);
        }

        public async Task<NotaFiscal> FindByCodigoDocumentoAsync(long codigoDocumento)
        {
            return await Query.SingleOrDefaultAsync(_ => _.CodDocumento == codigoDocumento);
        }
    }
}