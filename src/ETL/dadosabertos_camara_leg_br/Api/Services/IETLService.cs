using System.Data;
using System.Threading.Tasks;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api.Services
{
    public interface IETLService
    {
        Task ExecutePartido();
        Task ExecuteLegislatura();
        Task ExecuteDeputado();
        Task ExecuteDespesa();
        Task ExecuteDespesa(int year, int month);
    }
}