using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DeOlho.ETL.EFCore.Destinations;
using DeOlho.ETL.EFCore.Sources;
using DeOlho.ETL.Sources;
using DeOlho.ETL.Transforms;
using DeOlho.ETL.tse_jus_br.Api;
using DeOlho.ETL.tse_jus_br.Api.Domain;
using DeOlho.ETL.tse_jus_br.Api.Infrastructure.Data;
using DeOlho.EventBus.Services.Politicos.Messages;
using RawRabbit;

namespace DeOlho.ETL.tse_jus_br.Api.Services
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

        public async Task ExecuteETL(int year)
        {
            try
            {

                var cultureInfo = new CultureInfo("pt-BR");

                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

                var load = await new Process()
                    .Extract(() => new HttpStreamSource(this._httpClient, string.Format(this._configuration.PoliticosUrl, year)))
                    .TransformDescompressStream()
                    .TransformToList(_ => _.Value)
                    .Where(_ => _.Value.Name.ToUpper() == "CONSULTA_CAND_2018_BRASIL.CSV")
                    .ToStepCollection()
                    .Transform(_ => _.Value.Stream)
                    .TransformCsvToDynamic(";")
                    .TransformToList(_ => new List<dynamic>(_.Value))
                    .Transform<Politico>(_ => Transform(_.Value))
                    .Where(_ => _.Value.CD_CARGO == 6 && (_.Value.CD_SIT_TOT_TURNO == 2 || _.Value.CD_SIT_TOT_TURNO == 3))
                    .ToStepCollection()
                    .Load(() => new DbContextDestination(_deOlhoDbContext));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public Politico Transform(dynamic _)
        {
            
            return new Politico {
                DT_GERACAO = DateTime.Parse(_.DT_GERACAO),
                HH_GERACAO = DateTime.Parse(_.HH_GERACAO),
                ANO_ELEICAO = int.Parse(_.ANO_ELEICAO),
                CD_TIPO_ELEICAO = int.Parse(_.CD_TIPO_ELEICAO),
                NM_TIPO_ELEICAO = _.NM_TIPO_ELEICAO,
                NR_TURNO = int.Parse(_.NR_TURNO),
                CD_ELEICAO = int.Parse(_.CD_ELEICAO),
                DS_ELEICAO = _.DS_ELEICAO,
                DT_ELEICAO = DateTime.Parse(_.DT_ELEICAO),
                TP_ABRANGENCIA = _.TP_ABRANGENCIA,
                SG_UF = _.SG_UF,
                SG_UE = _.SG_UE,
                NM_UE = _.NM_UE,
                CD_CARGO = int.Parse(_.CD_CARGO),
                DS_CARGO = _.DS_CARGO,
                SQ_CANDIDATO = long.Parse(_.SQ_CANDIDATO),
                NR_CANDIDATO = int.Parse(_.NR_CANDIDATO),
                NM_CANDIDATO = _.NM_CANDIDATO,
                NM_URNA_CANDIDATO = _.NM_URNA_CANDIDATO,
                NM_SOCIAL_CANDIDATO = _.NM_SOCIAL_CANDIDATO,
                NR_CPF_CANDIDATO = long.Parse(_.NR_CPF_CANDIDATO),
                NM_EMAIL = _.NM_EMAIL,
                CD_SITUACAO_CANDIDATURA = int.Parse(_.CD_SITUACAO_CANDIDATURA),
                DS_SITUACAO_CANDIDATURA = _.DS_SITUACAO_CANDIDATURA,
                CD_DETALHE_SITUACAO_CAND = int.Parse(_.CD_DETALHE_SITUACAO_CAND),
                DS_DETALHE_SITUACAO_CAND = _.DS_DETALHE_SITUACAO_CAND,
                TP_AGREMIACAO = _.TP_AGREMIACAO,
                NR_PARTIDO = int.Parse(_.NR_PARTIDO),
                SG_PARTIDO = _.SG_PARTIDO,
                NM_PARTIDO = _.NM_PARTIDO,
                SQ_COLIGACAO = long.Parse(_.SQ_COLIGACAO),
                NM_COLIGACAO = _.NM_COLIGACAO,
                DS_COMPOSICAO_COLIGACAO = _.DS_COMPOSICAO_COLIGACAO,
                CD_NACIONALIDADE = int.Parse(_.CD_NACIONALIDADE),
                DS_NACIONALIDADE = _.DS_NACIONALIDADE,
                SG_UF_NASCIMENTO = _.SG_UF_NASCIMENTO,
                CD_MUNICIPIO_NASCIMENTO = int.Parse(_.CD_MUNICIPIO_NASCIMENTO),
                NM_MUNICIPIO_NASCIMENTO = _.NM_MUNICIPIO_NASCIMENTO,
                DT_NASCIMENTO = DateTime.Parse(_.DT_NASCIMENTO),
                NR_IDADE_DATA_POSSE = int.Parse(_.NR_IDADE_DATA_POSSE),
                NR_TITULO_ELEITORAL_CANDIDATO = long.Parse(_.NR_TITULO_ELEITORAL_CANDIDATO),
                CD_GENERO = int.Parse(_.CD_GENERO),
                DS_GENERO = _.DS_GENERO,
                CD_GRAU_INSTRUCAO = int.Parse(_.CD_GRAU_INSTRUCAO),
                DS_GRAU_INSTRUCAO = _.DS_GRAU_INSTRUCAO,
                CD_ESTADO_CIVIL = int.Parse(_.CD_ESTADO_CIVIL),
                DS_ESTADO_CIVIL = _.DS_ESTADO_CIVIL,
                CD_COR_RACA = int.Parse(_.CD_COR_RACA),
                DS_COR_RACA = _.DS_COR_RACA,
                CD_OCUPACAO = int.Parse(_.CD_OCUPACAO),
                DS_OCUPACAO = _.DS_OCUPACAO,
                NR_DESPESA_MAX_CAMPANHA = int.Parse(_.NR_DESPESA_MAX_CAMPANHA),
                CD_SIT_TOT_TURNO = int.Parse(_.CD_SIT_TOT_TURNO),
                DS_SIT_TOT_TURNO = _.DS_SIT_TOT_TURNO,
                ST_REELEICAO = _.ST_REELEICAO,
                ST_DECLARAR_BENS = _.ST_DECLARAR_BENS,
                NR_PROTOCOLO_CANDIDATURA = int.Parse(_.NR_PROTOCOLO_CANDIDATURA),
                NR_PROCESSO = long.Parse(_.NR_PROCESSO)
            };
        }


        // public bool Equals(Politico o, Politico n)
        // {
        //     return o == null
        //         || o.CPF != n.CPF
        //         || o.DataFalecimento != n.DataFalecimento
        //         || o.DataNascimento != n.DataNascimento
        //         || o.Escolaridade != n.Escolaridade
        //         || o.MunicipioNascimento != n.MunicipioNascimento
        //         || o.NomeCivil != n.NomeCivil
        //         || o.RedeSocial != n.RedeSocial
        //         || o.Sexo != n.Sexo
        //         || o.UFNascimento != n.UFNascimento
        //         || o.URLWebsite != n.URLWebsite
        //         || o.LegislaturaId != n.LegislaturaId
        //         || o.Nome != n.Nome
        //         || o.NomeEleitoral != n.NomeEleitoral
        //         || o.SiglaPartido != n.SiglaPartido
        //         || o.SiglaUf != n.SiglaUf
        //         || o.Situacao != n.Situacao
        //         || o.PartidoId != n.PartidoId
        //         || o.URLFoto != n.URLFoto
        //         || o.CondicaoEleitoral != n.CondicaoEleitoral
        //         || o.Data != n.Data
        //         || o.DescricaoStatus != n.DescricaoStatus
        //         || o.GabineteAndar != n.GabineteAndar
        //         || o.GabineteEmail != n.GabineteEmail
        //         || o.GabineteNome != n.GabineteNome
        //         || o.GabinetePredio != n.GabinetePredio
        //         || o.GabineteSala != n.GabineteSala
        //         || o.GabineteTelefone != n.GabineteTelefone;
        // }

        // public Politico Transform(dynamic _)
        // {
        //     return new Politico {
        //         Id = (long)_.Value.dados.id,
        //         CPF = (string)_.Value.dados.cpf,
        //         DataFalecimento = (DateTime?)_.Value.dados.dataFalecimento,
        //         DataNascimento = (DateTime)_.Value.dados.dataNascimento,
        //         Escolaridade = (string)_.Value.dados.escolaridade,
        //         MunicipioNascimento = (string)_.Value.dados.municipioNascimento,
        //         NomeCivil = (string)_.Value.dados.nomeCivil,
        //         RedeSocial = string.Join(",", ((IList)_.Value.dados.redeSocial).OfType<string>()),
        //         Sexo = (string)_.Value.dados.sexo,
        //         UFNascimento = (string)_.Value.dados.ufNascimento,
        //         URLWebsite = (string)_.Value.dados.urlWebsite,
        //         LegislaturaId  = (long)_.Value.dados.ultimoStatus.idLegislatura,
        //         Nome = (string)_.Value.dados.ultimoStatus.nome,
        //         NomeEleitoral  = (string)_.Value.dados.ultimoStatus.nomeEleitoral,
        //         SiglaPartido  = (string)_.Value.dados.ultimoStatus.siglaPartido,
        //         SiglaUf = (string)_.Value.dados.ultimoStatus.siglaUf,
        //         Situacao = (string)_.Value.dados.ultimoStatus.situacao,
        //         PartidoId = Convert.ToInt64(((string)_.Value.dados.ultimoStatus.uriPartido).Split('/').LastOrDefault()),
        //         URLFoto = (string)_.Value.dados.ultimoStatus.urlFoto,
        //         CondicaoEleitoral = (string)_.Value.dados.ultimoStatus.condicaoEleitoral,
        //         Data = (DateTime?)_.Value.dados.ultimoStatus.data,
        //         DescricaoStatus = (string)_.Value.dados.ultimoStatus.descricaoStatus,
        //         GabineteAndar = (string)_.Value.dados.ultimoStatus.gabinete.andar,
        //         GabineteEmail = (string)_.Value.dados.ultimoStatus.gabinete.email,
        //         GabineteNome = (string)_.Value.dados.ultimoStatus.gabinete.nome,
        //         GabinetePredio = (string)_.Value.dados.ultimoStatus.gabinete.predio,
        //         GabineteSala = (string)_.Value.dados.ultimoStatus.gabinete.sala,
        //         GabineteTelefone = (string)_.Value.dados.ultimoStatus.gabinete.telefone
        //     };
        // }

        // public PoliticoChangeMessage Message(Politico politico)
        // {
        //     var message =  new PoliticoChangeMessage {
        //         CPF = politico.CPF,
        //         Nome = politico.NomeCivil,
        //         Apelido = politico.NomeEleitoral,
        //         Falecimento = politico.DataFalecimento,
        //         Nascimento = politico.DataNascimento,
        //         NascimentoMunicipio = politico.MunicipioNascimento,
        //         NascimentoUF = politico.UFNascimento,
        //         Escolaridade = MessageEscolaridade(politico.Escolaridade),
        //         Sexo = MessageSexo(politico.Sexo),
        //         Partido = politico.SiglaPartido,
        //         MandatoTipo = 1,
        //         MandatoInicio = legislatura.DataInicio,
        //         MandatoFim = legislatura.DataFim,
        //         MandatoSituacao = MessageMandatoSituacao(politico.Situacao),
        //         Contatos = new List<PoliticoChangeMessage.Contato>()
        //     };

        //     if (!string.IsNullOrEmpty(politico.RedeSocial))
        //     {
        //         message.Contatos.Add(new PoliticoChangeMessage.Contato { Tipo = 1, Valor = politico.RedeSocial  });
        //     }

        //     if (!string.IsNullOrEmpty(politico.URLWebsite))
        //     {
        //         message.Contatos.Add(new PoliticoChangeMessage.Contato { Tipo = 1, Valor = politico.RedeSocial  });
        //     }

        //     if (!string.IsNullOrEmpty(politico.URLWebsite))
        //     {
        //         message.Contatos.Add(new PoliticoChangeMessage.Contato { Tipo = 1, Valor = politico.URLWebsite  });
        //     }

        //     if (!string.IsNullOrEmpty(politico.GabineteEmail))
        //     {
        //         message.Contatos.Add(new PoliticoChangeMessage.Contato { Tipo = 1, Valor = politico.GabineteEmail  });
        //     }

        //     if (!string.IsNullOrEmpty(politico.GabineteTelefone))
        //     {
        //         message.Contatos.Add(new PoliticoChangeMessage.Contato { Tipo = 1, Valor = politico.GabineteTelefone  });
        //     }

        //     return message;
        // }

        // public int MessageEscolaridade(string escolaridade)
        // {
        //     switch (escolaridade)
        //     {
        //         case null:
        //         case "":
        //             return 0;
        //         case "Ensino Fundamental Incompleto":
        //         case "Primário Incompleto":
        //             return 10;
        //         case "Ensino Fundamental":
        //         case "Primário":
        //             return 15;
        //         case "Ensino Médio Incompleto":
        //         case "Secundário Incompleto":
        //             return 20;
        //         case "Ensino Médio":
        //         case "Secundário":
        //             return 25;
        //         case "Ensino Técnico Incompleto":
        //             return 30;
        //         case "Ensino Técnico":
        //             return 35;
        //         case "Superior Incompleto":
        //             return 40;
        //         case "Superior":
        //             return 45;
        //         case "Pós-Graduação Incompleto":
        //             return 50;
        //         case "Pós-Graduação":
        //             return 55;
        //         case "Mestrado Incompleto":
        //             return 60;
        //         case "Mestrado":
        //             return 65;
        //         case "Doutorado Incompleto":
        //             return 70;
        //         case "Doutorado":
        //             return 75;
        //         default:
        //             return 99;
        //     }
        // }

        // public int MessageSexo(string sexo)
        // {
        //     switch (sexo)
        //     {
        //         case null:
        //         case "":
        //             return 1;
        //         case "M":
        //             return 2;
        //         case "F":
        //             return 4;
        //         default:
        //             return 99;
        //     }
        // }

        // public int MessageMandatoSituacao(string mandatoSituacao)
        // {
        //     switch (mandatoSituacao)
        //     {
        //         case null:
        //         case "":
        //             return 0;
        //         case "E":
        //         case "Exercício":
        //             return 1;
        //         case "A":
        //         case "Afastado":
        //             return 2;
        //         case "C":
        //         case "Convocado":
        //             return 3;
        //         case "F":
        //         case "Fim de Mandato":
        //             return 4;
        //         case "L":
        //         case "Licença":
        //             return 5;
        //         case "S":
        //         case "Suplência":
        //             return 6;
        //         case "U":
        //         case "Suspenso":
        //             return 7;
        //         case "V":
        //         case "Vacância":
        //             return 8;
        //         default:
        //             return 99;
        //     }
        // }


    }
}