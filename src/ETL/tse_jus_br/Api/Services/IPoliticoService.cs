using System.Threading.Tasks;

namespace DeOlho.ETL.tse_jus_br.Api.Services
{
    public interface IPoliticoService
    {
         Task ExecuteETL(int year);
    }
}