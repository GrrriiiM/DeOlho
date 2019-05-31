using System.Threading.Tasks;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api.Interfaces
{
    public interface ILegislaturaService
    {
         Task ExecuteETL();
    }
}