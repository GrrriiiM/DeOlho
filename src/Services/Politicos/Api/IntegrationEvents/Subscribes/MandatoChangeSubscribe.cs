using System.Linq;
using System.Threading.Tasks;
using DeOlho.EventBus.Services.Politicos.Messages;
using DeOlho.Services.Politicos.Api.Domain;
using DeOlho.Services.Politicos.Api.Infrastructure.Data;

namespace DeOlho.Services.Politicos.Api.IntegrationEvents.Subscribes
{
    public class MandatoChangeSubscribe : IMandatoChangeSubscribe
    {
        readonly DeOlhoDbContext _dbContext;
        public MandatoChangeSubscribe(
            DeOlhoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(MandatoChangeMessage message)
        {
            var exist = _dbContext.Mandatos.Any(_ => 
                _.DataInicio == message.DataInicio
                && _.DataFim == message.DataFim
                && (int)_.Tipo == message.Tipo);
                
            if (!exist)
            {
                var mandato = new Mandato(message.DataInicio, message.DataFim, (MandatoTipo)message.Tipo);
                mandato.SetIntegration(
                    message.Timestamp,
                    message.Origin,
                    message.Id);
                await _dbContext.Mandatos.AddAsync(mandato);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}