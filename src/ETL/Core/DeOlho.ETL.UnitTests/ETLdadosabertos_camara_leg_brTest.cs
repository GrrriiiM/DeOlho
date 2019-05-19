using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DeOlho.ETL.dadosabertos_camara_leg_br;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Xunit;

namespace DeOlho.ETL.UnitTests
{
    public class ETLdadosabertos_camara_leg_brTest
    {
        readonly Mock<IIntegrationServiceConfiguration> _configurationMock;

        readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;

        readonly IntegrationService _integrationService;

        public ETLdadosabertos_camara_leg_brTest()
        {
            _configurationMock = new Mock<IIntegrationServiceConfiguration>();

            _configurationMock.SetupGet(_ => _.PartidoURL).Returns($"{Constants.Url.ROOT}{Constants.Url.PARTIDO}");
            _configurationMock.SetupGet(_ => _.PartidoDetailWithIdArgURL).Returns($"{Constants.Url.ROOT}{Constants.Url.DETAIL_PARTIDO}");
            _configurationMock.SetupGet(_ => _.PartidoTableName).Returns(Constants.Db.TABLE_PARTIDO);

            _configurationMock.SetupGet(_ => _.LegislaturaURL).Returns($"{Constants.Url.ROOT}{Constants.Url.LEGISLATURA}");
            _configurationMock.SetupGet(_ => _.LegislaturaTableName).Returns(Constants.Db.TABLE_LEGISLATURA);

            _configurationMock.SetupGet(_ => _.DeputadoURL).Returns($"{Constants.Url.ROOT}{Constants.Url.DEPUTADO}");
            _configurationMock.SetupGet(_ => _.DeputadoDetailWithIdArgURL).Returns($"{Constants.Url.ROOT}{Constants.Url.DETAIL_DEPUTADO}");
            _configurationMock.SetupGet(_ => _.DeputadoTableName).Returns(Constants.Db.TABLE_DEPUTADO);

            _configurationMock.SetupGet(_ => _.DespesaDetailWithIdMonthYeahArgURL).Returns($"{Constants.Url.ROOT}{Constants.Url.DETAIL_DESPESA}");
            _configurationMock.SetupGet(_ => _.DespesaTableName).Returns(Constants.Db.TABLE_DESPESA);

            _integrationService = new IntegrationService(new HttpClient(new FakeHttpMessageHandler(_configurationMock.Object)), _configurationMock.Object);
        }


        public class FakeHttpMessageHandler : DelegatingHandler
        {
            private IIntegrationServiceConfiguration _configuration;

            public FakeHttpMessageHandler(IIntegrationServiceConfiguration configuration)
            {
                _configuration = configuration;   
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var json = "";
                var requestUri = request.RequestUri.ToString().ToUpper();

                if (requestUri == _configuration.PartidoURL.ToUpper())
                    json = Constants.Json.PARTIDO;
                else if (requestUri == string.Format(_configuration.PartidoDetailWithIdArgURL, 1).ToUpper())
                    json = Constants.Json.PARTIDO_1;
                else if (requestUri == string.Format(_configuration.PartidoDetailWithIdArgURL, 2).ToUpper())
                    json = Constants.Json.PARTIDO_2;
                else if (requestUri == _configuration.LegislaturaURL.ToUpper())
                    json = Constants.Json.LEGISLATURA;
                if (requestUri == _configuration.DeputadoURL.ToUpper())
                    json = Constants.Json.DEPUTADO;
                else if (requestUri == string.Format(_configuration.DeputadoDetailWithIdArgURL, 204536).ToUpper())
                    json = Constants.Json.DEPUTADO_1;
                else if (requestUri == string.Format(_configuration.DeputadoDetailWithIdArgURL, 92346).ToUpper())
                    json = Constants.Json.DEPUTADO_2;
                else if (requestUri == string.Format(_configuration.DespesaDetailWithIdMonthYeahArgURL, 204536, 2019, 3).ToUpper())
                    json = Constants.Json.DESPESA_1;
                else if (requestUri == string.Format(_configuration.DespesaDetailWithIdMonthYeahArgURL, 92346, 2019, 3).ToUpper())
                    json = Constants.Json.DESPESA_2;

                await Task.Delay(1);

                return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = new StringContent(json) };
            }
        }

        private async Task integrationService_Test(Func<IDbConnection, IDbTransaction, Task> execute, string createTable, string insertTable, string deleteTable)
        {
            var dbConnectionMock = new Mock<IDbConnection>();
            var dbTransactionMock = new Mock<IDbTransaction>();
            var dbCommandMock = new Mock<IDbCommand>();

            dbCommandMock.SetupAllProperties();

            dbConnectionMock
            .Setup(_=> _.CreateCommand())
            .Returns(dbCommandMock.Object);

            dbCommandMock
            .Setup(_ => _.ExecuteScalar())
            .Returns(() => {
                throw new Exception();
            });
            
            dbCommandMock
            .Setup(_ => _.ExecuteNonQuery())
            .Returns(() => {
                return 0;
            });

            await execute(dbConnectionMock.Object, dbTransactionMock.Object);
            
            createTable = createTable.ToUpper().Replace("\r","").Replace("\n","").Replace("\t","").Replace(" ","");
            insertTable = insertTable.ToUpper().Replace("\r","").Replace("\n","").Replace("\t","").Replace(" ","");
            deleteTable = deleteTable.ToUpper().Replace("\r","").Replace("\n","").Replace("\t","").Replace(" ","");

            dbCommandMock.VerifySet(_ => _.CommandText = It.Is<string>(_1 => _1.ToUpper().Replace("\r","").Replace("\n","").Replace("\t","").Replace(" ","") == createTable), Times.Once);
            dbCommandMock.VerifySet(_ => _.CommandText = It.Is<string>(_1 => _1.ToUpper().Replace("\r","").Replace("\n","").Replace("\t","").Replace(" ","") == insertTable), Times.Once);
            dbCommandMock.VerifySet(_ => _.CommandText = It.Is<string>(_1 => _1.ToUpper().Replace("\r","").Replace("\n","").Replace("\t","").Replace(" ","") == deleteTable), Times.Once);
        }

        [Fact]
        public async void IntegrationService_ExecutePartido()
        {
            await integrationService_Test(_integrationService.ExecutePartido, Constants.Db.CREATE_PARTIDO, Constants.Db.INSERT_PARTIDO, Constants.Db.DELETE_PARTIDO);
        }

        [Fact]
        public async void IntegrationService_ExecuteLegislatura()
        {
            await integrationService_Test(_integrationService.ExecuteLegislatura, Constants.Db.CREATE_LEGISLATURA, Constants.Db.INSERT_LEGISLATURA, Constants.Db.DELETE_LEGISLATURA);
        }

        [Fact]
        public async void IntegrationService_ExecuteDeputado()
        {
            await integrationService_Test(_integrationService.ExecuteDeputado, Constants.Db.CREATE_DEPUTADO, Constants.Db.INSERT_DEPUTADO, Constants.Db.DELETE_DEPUTADO);
        }

        [Fact]
        public async void IntegrationService_ExecuteDespesa()
        {
            await integrationService_Test(async (dbConnection, dbTransaction) => await _integrationService.ExecuteDespesa(dbConnection, dbTransaction, 2019, 3), Constants.Db.CREATE_DESPESA, Constants.Db.INSERT_DESPESA, string.Format(Constants.Db.DELETE_DESPESA, 2019, 3));
        }
    }
}