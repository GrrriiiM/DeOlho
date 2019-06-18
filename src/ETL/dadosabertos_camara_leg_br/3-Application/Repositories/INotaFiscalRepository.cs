using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_leg_br.Domain;

namespace DeOlho.ETL.dadosabertos_leg_br.Infrastructure.Repositories
{
    public interface INotaFiscalRepository : IRepository<NotaFiscal>
    {
        Task<NotaFiscal> FindByCodigoDocumentoAsync(long codigoDocumento);
        NotaFiscal FindByCodigoDocumento(long codigoDocumento);
    }
}