using System.Data;
using System.Threading.Tasks;

namespace DeOlho.ETL.dadosabertos_camara_leg_br
{
    public interface IIntegrationService
    {
        Task ExecutePartido(IDbConnection destinationDbConnection, IDbTransaction destinationDbTransaction);
        Task ExecuteLegislatura(IDbConnection destinationDbConnection, IDbTransaction destinationDbTransaction);

        Task ExecuteDeputado(IDbConnection destinationDbConnection, IDbTransaction destinationDbTransaction);
    }
}