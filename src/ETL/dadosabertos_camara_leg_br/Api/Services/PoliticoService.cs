using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Domain;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Infrastructure.Data;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Interfaces;
using DeOlho.ETL.EFCore.Destinations;
using DeOlho.ETL.EFCore.Sources;
using DeOlho.ETL.Sources;
using DeOlho.ETL.Transforms;
using DeOlho.EventBus.Services.Politicos.Messages;
using RawRabbit;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api.Services
{
    public class PoliticoService : IPoliticoService
    {
        readonly IETLConfiguration _configuration;
        readonly DeOlhoDbContext _deOlhoDbContext;
        readonly HttpClient _httpClient;
        readonly IBusClient _busClient;
        public PoliticoService(
            HttpClient httpClient,
            DeOlhoDbContext deOlhoDbContext,
            IETLConfiguration configuration,
            IBusClient busClient)
        {
            _httpClient =  httpClient;
            _deOlhoDbContext = deOlhoDbContext;
            _configuration = configuration;
            _busClient = busClient;
        }

        public async Task ExecuteETL()
        {
            try
            {
                var load = await new Process()
                    .Extract(() => new HttpJsonSource(this._httpClient, this._configuration.DeputadoURL))
                    .TransformJsonToDynamic()
                    .TransformToList(_ => new List<dynamic>(_.Value.dados))
                    .Extract(_ => new HttpJsonSource(this._httpClient, string.Format(this._configuration.DeputadoDetailWithIdArgURL, _.Value.id)))
                    .TransformJsonToDynamic()
                    .Transform(Transform)
                    .Extract(_ => new DbContextSingleOrDefaultSource<Politico>(_deOlhoDbContext, _.Value.Id))
                    .Where(_ => Equals(_.Value, (Politico)_.Parent.Value))
                    .Select(_ => (Politico)_.Parent.Value)
                    .ParallelForEach(_ => _busClient.PublishAsync(Message(_)))
                    .ToStepCollection()
                    .Load(() => new DbContextDestination(_deOlhoDbContext));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public bool Equals(Politico o, Politico n)
        {
            return o == null
                || o.CPF != n.CPF
                || o.DataFalecimento != n.DataFalecimento
                || o.DataNascimento != n.DataNascimento
                || o.Escolaridade != n.Escolaridade
                || o.MunicipioNascimento != n.MunicipioNascimento
                || o.NomeCivil != n.NomeCivil
                || o.RedeSocial != n.RedeSocial
                || o.Sexo != n.Sexo
                || o.UFNascimento != n.UFNascimento
                || o.URLWebsite != n.URLWebsite
                || o.LegislaturaId != n.LegislaturaId
                || o.Nome != n.Nome
                || o.NomeEleitoral != n.NomeEleitoral
                || o.SiglaPartido != n.SiglaPartido
                || o.SiglaUf != n.SiglaUf
                || o.Situacao != n.Situacao
                || o.PartidoId != n.PartidoId
                || o.URLFoto != n.URLFoto
                || o.CondicaoEleitoral != n.CondicaoEleitoral
                || o.Data != n.Data
                || o.DescricaoStatus != n.DescricaoStatus
                || o.GabineteAndar != n.GabineteAndar
                || o.GabineteEmail != n.GabineteEmail
                || o.GabineteNome != n.GabineteNome
                || o.GabinetePredio != n.GabinetePredio
                || o.GabineteSala != n.GabineteSala
                || o.GabineteTelefone != n.GabineteTelefone;
        }

        public Politico Transform(dynamic _)
        {
            return new Politico {
                Id = (long)_.Value.dados.id,
                CPF = (string)_.Value.dados.cpf,
                DataFalecimento = (DateTime?)_.Value.dados.dataFalecimento,
                DataNascimento = (DateTime)_.Value.dados.dataNascimento,
                Escolaridade = (string)_.Value.dados.escolaridade,
                MunicipioNascimento = (string)_.Value.dados.municipioNascimento,
                NomeCivil = (string)_.Value.dados.nomeCivil,
                RedeSocial = string.Join(",", ((IList)_.Value.dados.redeSocial).OfType<string>()),
                Sexo = (string)_.Value.dados.sexo,
                UFNascimento = (string)_.Value.dados.ufNascimento,
                URLWebsite = (string)_.Value.dados.urlWebsite,
                LegislaturaId  = (long)_.Value.dados.ultimoStatus.idLegislatura,
                Nome = (string)_.Value.dados.ultimoStatus.nome,
                NomeEleitoral  = (string)_.Value.dados.ultimoStatus.nomeEleitoral,
                SiglaPartido  = (string)_.Value.dados.ultimoStatus.siglaPartido,
                SiglaUf = (string)_.Value.dados.ultimoStatus.siglaUf,
                Situacao = (string)_.Value.dados.ultimoStatus.situacao,
                PartidoId = Convert.ToInt64(((string)_.Value.dados.ultimoStatus.uriPartido).Split('/').LastOrDefault()),
                URLFoto = (string)_.Value.dados.ultimoStatus.urlFoto,
                CondicaoEleitoral = (string)_.Value.dados.ultimoStatus.condicaoEleitoral,
                Data = (DateTime?)_.Value.dados.ultimoStatus.data,
                DescricaoStatus = (string)_.Value.dados.ultimoStatus.descricaoStatus,
                GabineteAndar = (string)_.Value.dados.ultimoStatus.gabinete.andar,
                GabineteEmail = (string)_.Value.dados.ultimoStatus.gabinete.email,
                GabineteNome = (string)_.Value.dados.ultimoStatus.gabinete.nome,
                GabinetePredio = (string)_.Value.dados.ultimoStatus.gabinete.predio,
                GabineteSala = (string)_.Value.dados.ultimoStatus.gabinete.sala,
                GabineteTelefone = (string)_.Value.dados.ultimoStatus.gabinete.telefone
            };
        }

        public PoliticoChangeMessage Message(Politico politico, Legislatura legislatura)
        {
            var message =  new PoliticoChangeMessage {
                CPF = politico.CPF,
                Nome = politico.NomeCivil,
                Apelido = politico.NomeEleitoral,
                Falecimento = politico.DataFalecimento,
                Nascimento = politico.DataNascimento,
                NascimentoMunicipio = politico.MunicipioNascimento,
                NascimentoUF = politico.UFNascimento,
                Escolaridade = MessageEscolaridade(politico.Escolaridade),
                Sexo = MessageSexo(politico.Sexo),
                Partido = politico.SiglaPartido,
                MandatoTipo = 1,
                MandatoInicio = legislatura.DataInicio,
                MandatoFim = legislatura.DataFim,
                MandatoSituacao = MessageMandatoSituacao(politico.Situacao),
                Contatos = new List<PoliticoChangeMessage.Contato>()
            };

            if (!string.IsNullOrEmpty(politico.RedeSocial))
            {
                message.Contatos.Add(new PoliticoChangeMessage.Contato { Tipo = 1, Valor = politico.RedeSocial  });
            }

            if (!string.IsNullOrEmpty(politico.URLWebsite))
            {
                message.Contatos.Add(new PoliticoChangeMessage.Contato { Tipo = 1, Valor = politico.RedeSocial  });
            }

            if (!string.IsNullOrEmpty(politico.URLWebsite))
            {
                message.Contatos.Add(new PoliticoChangeMessage.Contato { Tipo = 1, Valor = politico.URLWebsite  });
            }

            if (!string.IsNullOrEmpty(politico.GabineteEmail))
            {
                message.Contatos.Add(new PoliticoChangeMessage.Contato { Tipo = 1, Valor = politico.GabineteEmail  });
            }

            if (!string.IsNullOrEmpty(politico.GabineteTelefone))
            {
                message.Contatos.Add(new PoliticoChangeMessage.Contato { Tipo = 1, Valor = politico.GabineteTelefone  });
            }

            return message;
        }

        public int MessageEscolaridade(string escolaridade)
        {
            switch (escolaridade)
            {
                case null:
                case "":
                    return 0;
                case "Ensino Fundamental Incompleto":
                case "Primário Incompleto":
                    return 10;
                case "Ensino Fundamental":
                case "Primário":
                    return 15;
                case "Ensino Médio Incompleto":
                case "Secundário Incompleto":
                    return 20;
                case "Ensino Médio":
                case "Secundário":
                    return 25;
                case "Ensino Técnico Incompleto":
                    return 30;
                case "Ensino Técnico":
                    return 35;
                case "Superior Incompleto":
                    return 40;
                case "Superior":
                    return 45;
                case "Pós-Graduação Incompleto":
                    return 50;
                case "Pós-Graduação":
                    return 55;
                case "Mestrado Incompleto":
                    return 60;
                case "Mestrado":
                    return 65;
                case "Doutorado Incompleto":
                    return 70;
                case "Doutorado":
                    return 75;
                default:
                    return 99;
            }
        }

        public int MessageSexo(string sexo)
        {
            switch (sexo)
            {
                case null:
                case "":
                    return 1;
                case "M":
                    return 2;
                case "F":
                    return 4;
                default:
                    return 99;
            }
        }

        public int MessageMandatoSituacao(string mandatoSituacao)
        {
            switch (mandatoSituacao)
            {
                case null:
                case "":
                    return 0;
                case "E":
                case "Exercício":
                    return 1;
                case "A":
                case "Afastado":
                    return 2;
                case "C":
                case "Convocado":
                    return 3;
                case "F":
                case "Fim de Mandato":
                    return 4;
                case "L":
                case "Licença":
                    return 5;
                case "S":
                case "Suplência":
                    return 6;
                case "U":
                case "Suspenso":
                    return 7;
                case "V":
                case "Vacância":
                    return 8;
                default:
                    return 99;
            }
        }

    }
}