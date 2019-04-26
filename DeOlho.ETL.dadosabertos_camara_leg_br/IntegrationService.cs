using System;
using System.Collections.Generic;
using System.Data;
using DeOlho.ETL.Sources;
using DeOlho.ETL.Transforms;
using DeOlho.ETL.Steps;
using DeOlho.ETL.Destinations;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;

namespace DeOlho.ETL.dadosabertos_camara_leg_br
{
    public class IntegrationService : IIntegrationService
    {
        readonly IIntegrationServiceConfiguration _configuration;

        public IntegrationService(
            IIntegrationServiceConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task ExecutePartido(IDbConnection destinationDbConnection, IDbTransaction destinationDbTransaction)
        {
                var load = await new Process()
                    .Extract(() => new HttpJsonSource(this._configuration.PartidoURL))
                    .TransformJsonToDynamic()
                    .TransformToList(_ => new List<dynamic>(_.dados))
                    .Extract(_ => new HttpJsonSource(string.Format(this._configuration.PartidoDetailWithIdArgURL, _.id)))
                    .TransformJsonToDynamic()
                    .Transform(_ => 
                        new {
                            Id = (long)_.dados.id,
                            Sigla = (string)_.dados.sigla,
                            Nome = (string)_.dados.nome,
                            Data = _.dados.status.data != null ? new Nullable<DateTime>(DateTime.Parse((string)_.dados.status.data)) : null,
                            LegislaturaId = (long)_.dados.status.idLegislatura,
                            Situacao = (string)_.dados.status.situacao,
                            TotalPosse = (int)_.dados.status.totalPosse,
                            TotalMembros = (int)_.dados.status.totalMembros,
                            LiderId = (string)_.dados.status.lider.uri,
                            UrlFacebook = (string)_.dados.urlFacebook,
                            UrlLogo = (string)_.dados.urlLogo,
                            UrlWebSite = (string)_.dados.urlWebSite
                        })
                    .DbCreateTableIfNotExist(destinationDbConnection, destinationDbTransaction, this._configuration.PartidoTableName)
                    .Load(() => new DbDestination(destinationDbConnection, destinationDbTransaction, this._configuration.PartidoTableName));
        }

        public async Task ExecuteLegislatura(IDbConnection destinationDbConnection, IDbTransaction destinationDbTransaction)
        {
                var load = await new Process()
                    .Extract(() => new HttpJsonSource(this._configuration.LegislaturaURL))
                    .TransformJsonToDynamic()
                    .TransformToList(_ => new List<dynamic>(_.dados))
                    .Transform(_ => 
                        new {
                            Id = (long)_.id,
                            DataInicio = (DateTime)_.dataInicio,
                            DataFim = (DateTime)_.dataInicio
                        })
                    .DbCreateTableIfNotExist(destinationDbConnection, destinationDbTransaction, this._configuration.LegislaturaTableName)
                    .Load(() => new DbDestination(destinationDbConnection, destinationDbTransaction, this._configuration.LegislaturaTableName));
        }


        public async Task ExecuteDeputado(IDbConnection destinationDbConnection, IDbTransaction destinationDbTransaction)
        {
                var load = await new Process()
                    .Extract(() => new HttpJsonSource(this._configuration.DeputadoURL))
                    .TransformJsonToDynamic()
                    .TransformToList(_ => new List<dynamic>(_.dados))
                    .Extract(_ => new HttpJsonSource(string.Format(this._configuration.DeputadoDetailWithIdArgURL, _.id)))
                    .TransformJsonToDynamic()
                    .Transform(_ => 
                        new {
                            Id = (long)_.dados.id,
                            CPF = (string)_.dados.cpf,
                            DataFalecimento = (DateTime?)_.dados.dataFalecimento,
                            DataNascimento = (DateTime)_.dados.dataNascimento,
                            Escolaridade = (string)_.dados.escolaridade,
                            MunicipioNascimento = (string)_.dados.municipioNascimento,
                            NomeCivil = (string)_.dados.nomeCivil,
                            RedeSocial = string.Join(",", ((IList)_.dados.redeSocial).OfType<string>()),
                            Sexo = (string)_.dados.sexo,
                            UFNascimento = (string)_.dados.ufNascimento,
                            URLWebsite = (string)_.dados.urlWebsite,
                            LegislaturaId  = (long)_.dados.ultimoStatus.idLegislatura,
                            Nome = (string)_.dados.ultimoStatus.nome,
                            NomeEleitoral  = (string)_.dados.ultimoStatus.nomeEleitoral,
                            SiglaPartido  = (string)_.dados.ultimoStatus.siglaPartido,
                            SiglaUf = (string)_.dados.ultimoStatus.siglaUf,
                            Situacao = (string)_.dados.ultimoStatus.situacao,
                            PartidoId = Convert.ToInt64(((string)_.dados.ultimoStatus.uriPartido).Split('/').LastOrDefault()),
                            URLFoto = (string)_.dados.ultimoStatus.urlFoto,
                            CondicaoEleitoral = (string)_.dados.ultimoStatus.condicaoEleitoral,
                            Data = (DateTime?)_.dados.ultimoStatus.data,
                            DescricaoStatus = (string)_.dados.ultimoStatus.descricaoStatus,
                            GabineteAndar = (string)_.dados.ultimoStatus.gabinete.andar,
                            GabineteEmail = (string)_.dados.ultimoStatus.gabinete.email,
                            GabineteNome = (string)_.dados.ultimoStatus.gabinete.nome,
                            GabinetePredio = (string)_.dados.ultimoStatus.gabinete.predio,
                            GabineteSala = (string)_.dados.ultimoStatus.gabinete.sala,
                            GabineteTelefone = (string)_.dados.ultimoStatus.gabinete.telefone
                            
                        })
                    .DbCreateTableIfNotExist(destinationDbConnection, destinationDbTransaction, this._configuration.DeputadoTableName)
                    .Load(() => new DbDestination(destinationDbConnection, destinationDbTransaction, this._configuration.DeputadoTableName));
        }

    }
}
