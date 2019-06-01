using System.Linq;
using System.Threading.Tasks;
using DeOlho.EventBus.Services.Politicos.Messages;
using DeOlho.Services.Politicos.Api.Domain;
using DeOlho.Services.Politicos.Api.Infrastructure.Data;

namespace DeOlho.Services.Politicos.Api.IntegrationEvents.Subscribes
{
    public class PartidoChangeSubscribe : IPartidoChangeSubscribe
    {
        readonly DeOlhoDbContext _dbContext;
        public PartidoChangeSubscribe(
            DeOlhoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(PartidoChangeMessage message)
        {
            var partido = _dbContext.Partidos.FirstOrDefault(_ => 
                _.Sigla == message.Sigla);
                
            if (partido == null)
            {
                partido = new Partido(message.Sigla, message.Nome)
                {
                    Situacao = (PartidoSituacao)message.Situacao,
                    UrlFoto = message.UrlFoto
                };
                partido.SetIntegration(
                    message.Timestamp,
                    message.Origin,
                    message.Id);
                message.Contatos.ForEach(_ => partido.AddContato((ContatoTipo)_.Tipo, _.Valor));
                await _dbContext.Partidos.AddAsync(partido);
            }
            else
            {
                partido.Situacao = (PartidoSituacao)message.Situacao;
                partido.UrlFoto = partido.UrlFoto;
                partido.SetIntegration(
                    message.Timestamp,
                    message.Origin,
                    message.Id);
                message.Contatos.ForEach(_ => {
                    var contato = partido.Contatos.FirstOrDefault(_1 => (int)_1.Tipo == _.Tipo);
                    if (contato == null)
                    {
                        partido.AddContato((ContatoTipo)_.Tipo, _.Valor);
                    }
                    else
                    {
                        if (contato.Contato != _.Valor)
                        {
                            contato.Contato = _.Valor;
                        }
                    }
                });
                _dbContext.Partidos.Update(partido);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}