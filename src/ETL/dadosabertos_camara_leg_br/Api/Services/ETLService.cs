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
using System.Net.Http;
using System.Data.Common;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Domains;
using DeOlho.ETL.EFCore.Destinations;
using DeOlho.ETL.dadosabertos_camara_leg_br.Api.Infra.Data;
using DeOlho.ETL.EFCore.Sources;
using RawRabbit;
using DeOlho.EventBus.Services.Politicos;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api.Services
{
    public class ETLService : IETLService
    {
        readonly IETLConfiguration _configuration;
        readonly DeOlhoDbContext _deOlhoDbContext;
        readonly HttpClient _httpClient;
        readonly IBusClient _busClient;
        public ETLService(
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

        public async Task ExecutePartido()
        {
            await execute(async (c, t) => await executePartido(c, t));
        }
        private async Task executePartido(IDbConnection destinationDbConnection, IDbTransaction destinationDbTransaction)
        {
                var load = await new Process()
                    .Extract(() => new HttpJsonSource(this._httpClient, this._configuration.PartidoURL))
                    .TransformJsonToDynamic()
                    .TransformToList(_ => new List<dynamic>(_.Value.dados))
                    .Extract(_ => new HttpJsonSource(this._httpClient, string.Format(this._configuration.PartidoDetailWithIdArgURL, _.Value.id)))
                    .TransformJsonToDynamic()
                    .Transform(_ => 
                        new {
                            Id = (long)_.Value.dados.id,
                            Sigla = (string)_.Value.dados.sigla,
                            Nome = (string)_.Value.dados.nome,
                            Data = _.Value.dados.status.data != null ? new Nullable<DateTime>(DateTime.Parse((string)_.Value.dados.status.data)) : null,
                            LegislaturaId = (long)_.Value.dados.status.idLegislatura,
                            Situacao = (string)_.Value.dados.status.situacao,
                            TotalPosse = (int)_.Value.dados.status.totalPosse,
                            TotalMembros = (int)_.Value.dados.status.totalMembros,
                            LiderId = (string)_.Value.dados.status.lider.uri,
                            UrlFacebook = (string)_.Value.dados.urlFacebook,
                            UrlLogo = (string)_.Value.dados.urlLogo,
                            UrlWebSite = (string)_.Value.dados.urlWebSite
                        })
                    .DbCreateTableIfNotExist(destinationDbConnection, destinationDbTransaction, this._configuration.PartidoTableName)
                    .DbDelete(destinationDbConnection, destinationDbTransaction, this._configuration.PartidoTableName)
                    .Load(() => new DbDestinationCollection(destinationDbConnection, destinationDbTransaction, this._configuration.PartidoTableName));
        }


        public async Task ExecuteLegislatura()
        {
            try
            {
                var load = await new Process()
                    .Extract(() => new HttpJsonSource(this._httpClient, this._configuration.LegislaturaURL))
                    .TransformJsonToDynamic()
                    .TransformToList(_ => new List<dynamic>(_.Value.dados))
                    .Transform(_ => 
                        new Legislatura {
                            Id = (long)_.Value.id,
                            DataInicio = (DateTime)_.Value.dataInicio,
                            DataFim = (DateTime)_.Value.dataFim
                        })
                    .Extract(_ => new DbContextSingleOrDefaultSource<Legislatura>(_deOlhoDbContext, _.Value.Id))
                    .Where(_ => compareLegislatura(_.Value, (Legislatura)_.Parent.Value))
                    .Select(_ => (Legislatura)_.Parent.Value)
                    .ParallelForEach(_ => {
                        _busClient.PublishAsync<LegislaturaMessage>(new LegislaturaMessage { Id = _.Id, DataInicio = _.DataInicio, DataFim = _.DataFim.Value });
                    })
                    .ToStepCollection()
                    .Load(() => new DbContextDestination(_deOlhoDbContext));
            }
            catch (System.Exception)
            {
                
                throw;
            }
        
        }

        private bool compareLegislatura(Legislatura o, Legislatura n)
        {
            return o == null || o.DataInicio != n.DataInicio || o.DataFim != n.DataFim;
        }


        public async Task ExecuteDeputado()
        {
            await execute(async (c, t) => await executeDeputado(c, t));
        }
        private async Task executeDeputado(IDbConnection destinationDbConnection, IDbTransaction destinationDbTransaction)
        {
            var load = await new Process()
                .Extract(() => new HttpJsonSource(this._httpClient, this._configuration.DeputadoURL))
                .TransformJsonToDynamic()
                .TransformToList(_ => new List<dynamic>(_.Value.dados))
                .Extract(_ => new HttpJsonSource(this._httpClient, string.Format(this._configuration.DeputadoDetailWithIdArgURL, _.Value.id)))
                .TransformJsonToDynamic()
                .Transform(_ => 
                    new {
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
                        
                    })
                .DbCreateTableIfNotExist(destinationDbConnection, destinationDbTransaction, this._configuration.DeputadoTableName)
                .DbDelete(destinationDbConnection, destinationDbTransaction, this._configuration.DeputadoTableName)
                .Load(() => new DbDestinationCollection(destinationDbConnection, destinationDbTransaction, this._configuration.DeputadoTableName));
        }

        public async Task ExecuteDespesa()
        {
            await execute(async (c, t) => await executeDespesa(c, t, DateTime.Now.Year, DateTime.Now.Month));
        }

        public async Task ExecuteDespesa(int year, int month)
        {
            await execute(async (c, t) => await executeDespesa(c, t, year, month));
        }
        private async Task executeDespesa(IDbConnection destinationDbConnection, IDbTransaction destinationDbTransaction, int year, int month)
        {
            var load = await new Process()
                .Extract(() => new HttpJsonSource(this._httpClient, this._configuration.DeputadoURL))
                .TransformJsonToDynamic()
                .TransformToList(_ => new List<dynamic>(_.Value.dados))
                .Extract(_ => new HttpJsonSource(this._httpClient, string.Format(this._configuration.DespesaDetailWithIdMonthYeahArgURL, _.Value.id, year, month)))
                .TransformJsonToDynamic()
                .TransformToList(_ => new List<dynamic>(_.Value.dados))
                .Transform(_ => 
                    new {
                        DeputadoId = (long)((dynamic)_.Parent.Parent.Parent.Value).id,
                        Ano = (int)_.Value.ano,
                        CnpjCpfFornecedor = (string)_.Value.cnpjCpfFornecedor,
                        CodDocumento = (long)_.Value.codDocumento,
                        CodLote = (long)_.Value.codLote,
                        CodTipoDocumento = (int?)_.Value.codTipoDocumento,
                        DataDocumento = (DateTime?)_.Value.dataDocumento,
                        Mes = (int)_.Value.mes,
                        NomeFornecedor = (string)_.Value.nomeFornecedor,
                        NumDocumento = (string)_.Value.numDocumento,
                        NumRessarcimento = (string)_.Value.numRessarcimento,
                        Parcela = (int)_.Value.parcela,
                        TipoDespesa = (string)_.Value.tipoDespesa,
                        TipoDocumento = (string)_.Value.tipoDocumento,
                        URLDocumento = (string)_.Value.urlDocumento,
                        ValorDocumento = (decimal)_.Value.valorDocumento,
                        ValorGlosa = (decimal)_.Value.valorGlosa,
                        ValorLiquido = (decimal)_.Value.valorLiquido
                    })
                .DbCreateTableIfNotExist(destinationDbConnection, destinationDbTransaction, this._configuration.DespesaTableName)
                .DbDelete(destinationDbConnection, destinationDbTransaction, this._configuration.DespesaTableName, $"ANO = {year} AND MES = {month}")
                .Load(() => new DbDestinationCollection(destinationDbConnection, destinationDbTransaction, this._configuration.DespesaTableName));
        }

        private async Task execute(Func<IDbConnection, IDbTransaction, Task> execute)
        {
            var _dbConnection = _configuration.CreateConnection();
            IDbTransaction transaction = null;
            try
            {
                _dbConnection.Open();
                transaction = _dbConnection.BeginTransaction();
                await execute(_dbConnection, transaction);
                transaction.Commit();
                transaction = null;
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                if (transaction != null)
                    transaction.Rollback();
                if (_dbConnection.State != ConnectionState.Closed)
                    _dbConnection.Close();
            }
        }
    }
}