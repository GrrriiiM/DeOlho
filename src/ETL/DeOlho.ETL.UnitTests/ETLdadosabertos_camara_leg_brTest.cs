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

        const string TESTE_API_URI = "http://teste.com/api/";
        public ETLdadosabertos_camara_leg_brTest()
        {
            _configurationMock = new Mock<IIntegrationServiceConfiguration>();

            _configurationMock.SetupGet(_ => _.PartidoURL).Returns($"{TESTE_API_URI}{nameof(_configurationMock.Object.PartidoURL)}");
            _configurationMock.SetupGet(_ => _.PartidoDetailWithIdArgURL).Returns($"{TESTE_API_URI}{nameof(_configurationMock.Object.PartidoDetailWithIdArgURL)}/{{0}}");
            _configurationMock.SetupGet(_ => _.PartidoTableName).Returns(nameof(_configurationMock.Object.PartidoTableName));

            _integrationService = new IntegrationService(new HttpClient(new FakeHttpMessageHandler(_configurationMock.Object)), _configurationMock.Object);
        }


        public class FakeHttpMessageHandler : DelegatingHandler
        {

            const string JSON_PARTIDO = @"{'dados':[{'id':1},{'id':1}]}";
            const string JSON_PARTIDO_1 = @"
{
  'dados': {
    'id': 1,
    'sigla': 'NOVO',
    'nome': 'Partido Novo',
    'uri': 'https://dadosabertos.camara.leg.br/api/v2/partidos/37901',
    'status': {
      'data': '2019-02-01T22:19',
      'idLegislatura': '56',
      'situacao': 'Ativo',
      'totalPosse': '8',
      'totalMembros': '8',
      'uriMembros': 'https://dadosabertos.camara.leg.br/api/v2/deputados?legislatura=56&partido=NOVO',
      'lider': {
        'uri': 'https://dadosabertos.camara.leg.br/api/v2/deputados/156190',
        'nome': 'MARCEL VAN HATTEM',
        'siglaPartido': 'NOVO',
        'uriPartido': 'https://dadosabertos.camara.leg.br/api/v2/partidos/37901',
        'uf': 'RS',
        'idLegislatura': 56,
        'urlFoto': 'http://www.camara.gov.br/internet/deputado/bandep/156190.jpg'
      }
    },
    'numeroEleitoral': null,
    'urlLogo': 'http://www.camara.leg.br/internet/Deputado/img/partidos/NOVO.gif',
    'urlWebSite': null,
    'urlFacebook': null
  },
  'links': [
    {
      'rel': 'self',
      'href': 'https://dadosabertos.camara.leg.br/api/v2/partidos/37901'
    }
  ]
}            
            ";

            const string JSON_PARTIDO_2  = @"
{
  'dados': {
    'id': 2,
    'sigla': 'PT',
    'nome': 'Partido dos Trabalhadores',
    'uri': 'https://dadosabertos.camara.leg.br/api/v2/partidos/36844',
    'status': {
      'data': '2019-02-01T14:12',
      'idLegislatura': '56',
      'situacao': 'Ativo',
      'totalPosse': '54',
      'totalMembros': '55',
      'uriMembros': 'https://dadosabertos.camara.leg.br/api/v2/deputados?legislatura=56&partido=PT',
      'lider': {
        'uri': 'https://dadosabertos.camara.leg.br/api/v2/deputados/74400',
        'nome': 'PAULO PIMENTA',
        'siglaPartido': 'PT',
        'uriPartido': 'https://dadosabertos.camara.leg.br/api/v2/partidos/36844',
        'uf': 'RS',
        'idLegislatura': 56,
        'urlFoto': 'http://www.camara.gov.br/internet/deputado/bandep/74400.jpg'
      }
    },
    'numeroEleitoral': null,
    'urlLogo': 'http://www.camara.leg.br/internet/Deputado/img/partidos/PT.gif',
    'urlWebSite': null,
    'urlFacebook': null
  },
  'links': [
    {
      'rel': 'self',
      'href': 'https://dadosabertos.camara.leg.br/api/v2/partidos/36844'
    }
  ]
}
            ";


            private IIntegrationServiceConfiguration _configuration;

            public FakeHttpMessageHandler(IIntegrationServiceConfiguration configuration)
            {
                _configuration = configuration;   
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var json = "";

                if (request.RequestUri.ToString().ToUpper() == _configuration.PartidoURL.ToUpper())
                    json = JSON_PARTIDO;
                if (request.RequestUri.ToString().ToUpper() == string.Format(_configuration.PartidoDetailWithIdArgURL, 1).ToUpper())
                    json = JSON_PARTIDO_1;
                if (request.RequestUri.ToString().ToUpper() == string.Format(_configuration.PartidoDetailWithIdArgURL, 2).ToUpper())
                    json = JSON_PARTIDO_2;

                return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = new StringContent(json) };
            }
        }

        [Fact]
        public async void IntegrationService_ExecutePartido()
        {

            var dbConnectionMock = new Mock<IDbConnection>();
            var dbTransactionMock = new Mock<IDbTransaction>();
            var dbCommandMock = new Mock<IDbCommand>();
            var stepCollectionMock = new Mock<IStepCollection<object>>();

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

            await _integrationService.ExecutePartido(dbConnectionMock.Object, dbTransactionMock.Object);

            dbCommandMock.VerifySet(_ => _.CommandText = It.Is<string>(_1 => _1.ToUpper().Contains($"CREATE TABLE {_configurationMock.Object.PartidoTableName}".ToUpper())), Times.Once);

            dbCommandMock.VerifySet(_ => _.CommandText = It.Is<string>(_1 => _1.ToUpper().Contains($"INSERT INTO {_configurationMock.Object.PartidoTableName}".ToUpper())), Times.Once);
        
        }

    }
}