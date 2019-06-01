using System.Threading.Tasks;
using DeOlho.EventBus.Services.Politicos.Messages;

namespace DeOlho.Services.Politicos.Api.IntegrationEvents.Subscribes
{
    public interface IMandatoChangeSubscribe
    {
         Task Execute(MandatoChangeMessage message);
    }
}