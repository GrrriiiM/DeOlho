using System.Linq;
using System.Threading.Tasks;
using DeOlho.EventBus.Services.Politicos.Messages;
using DeOlho.Services.Politicos.Api.Domain;
using DeOlho.Services.Politicos.Api.Infrastructure.Data;

namespace DeOlho.Services.Politicos.Api.IntegrationEvents.Subscribes
{
    public class PoliticoChangeSubscribe : IPoliticoChangeSubscribe
    {
        readonly DeOlhoDbContext _dbContext;
        public PoliticoChangeSubscribe(
            DeOlhoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(PoliticoChangeMessage message)
        {
            var politico = _dbContext.Politicos.FirstOrDefault(_ => 
                _.CPF == message.CPF);
                
            if (politico == null)
            {
                politico = new Politico(
                    message.CPF,
                    message.Nome,
                    message.Apelido,
                    message.Nascimento,
                    message.NascimentoUF,
                    message.NascimentoMunicipio)
                {
                    Escolaridade = (PoliticoEscolaridade)message.Escolaridade,
                    Falecimento = message.Falecimento,
                    Situacao = (PoliticoSituacao)message.Situacao,
                    Sexo = (PoliticoSexo)message.Sexo
                };
                politico.SetIntegration(
                    message.Timestamp,
                    message.Origin,
                    message.Id);
                message.Contatos.ForEach(_ => politico.AddContato((ContatoTipo)_.Tipo, _.Valor));
                await _dbContext.Politicos.AddAsync(politico);
            }
            else
            {
                politico.Escolaridade = (PoliticoEscolaridade)message.Escolaridade;
                politico.Falecimento = message.Falecimento;
                politico.Situacao = (PoliticoSituacao)message.Situacao;
                politico.Sexo = (PoliticoSexo)message.Sexo;
                politico.SetIntegration(
                    message.Timestamp,
                    message.Origin,
                    message.Id);
                message.Contatos.ForEach(_ => {
                    var contato = politico.Contatos.FirstOrDefault(_1 => (int)_1.Tipo == _.Tipo);
                    if (contato == null)
                    {
                        politico.AddContato((ContatoTipo)_.Tipo, _.Valor);
                    }
                    else
                    {
                        if (contato.Contato != _.Valor)
                        {
                            contato.Contato = _.Valor;
                        }
                    }
                });
                _dbContext.Politicos.Update(politico);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}