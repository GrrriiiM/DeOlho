using System;
using System.Linq;
using DeOlho.ETL.camara_leg_br.Domain;
using DeOlho.ETL.camara_leg_br.UnitTests.Helpers;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FluentAssertions;
using Xunit;

namespace DeOlho.ETL.camara_leg_br.UnitTests.Domain
{
    public class DeputadoFederalTest
    {

        
        
        private ISingleObjectBuilder<DeputadoFederal> deputadoFederalBuilder;
        private ISingleObjectBuilder<DeputadoFederalResponse> deputadoFederalResponseBuilder;

        public DeputadoFederalTest()
        {
            var builderSettings = new BuilderSettings();
            var builder = new Builder(builderSettings);

            var namer = new RandomValuePropertyNamer(
                new RandomGenerator(),
                new ReflectionUtil(), 
                true,
                DateTime.Now, 
                DateTime.Now.AddDays(10), 
                true, 
                builderSettings);

            
            builderSettings.SetPropertyNamerFor<DeputadoFederal>(namer);
            builderSettings.SetPropertyNamerFor<DeputadoFederalResponse>(namer);
            builderSettings.SetPropertyNamerFor<DeputadoFederalUltimoStatusResponse>(namer);
            builderSettings.SetPropertyNamerFor<DeputadoFederalUltimoStatusGabineteResponse>(namer);
            
            var random = new Random();
            deputadoFederalBuilder = builder.CreateNew<DeputadoFederal>();
            deputadoFederalResponseBuilder = builder.CreateNew<DeputadoFederalResponse>()
                .With(_ => _.ultimoStatus = builder.CreateNew<DeputadoFederalUltimoStatusResponse>().Build())
                .With(_ => _.ultimoStatus.urlFoto = FizzWare.NBuilder.Generators.GetRandom.Phrase(15))
                .With(_ => _.ultimoStatus.condicaoEleitoral = FizzWare.NBuilder.Generators.GetRandom.Phrase(15))
                .With(_ => _.ultimoStatus.gabinete = builder.CreateNew<DeputadoFederalUltimoStatusGabineteResponse>().Build());
        }

        [Fact]
        public void Constructor()
        {
            var deputadoFederalResponseMock = deputadoFederalResponseBuilder.Build();
            var deputadoFederal = new DeputadoFederal((dynamic)deputadoFederalResponseMock);

            deputadoFederal.CPF.Should().Be(deputadoFederalResponseMock.cpf);
            deputadoFederal.OrigemId.Should().Be(deputadoFederalResponseMock.id);
            deputadoFederal.URLWebsite.Should().Be(deputadoFederalResponseMock.urlWebsite);
            deputadoFederal.URLFoto.Should().Be(deputadoFederalResponseMock.ultimoStatus.urlFoto);
            deputadoFederal.CondicaoEleitoral.Should().Be(deputadoFederalResponseMock.ultimoStatus.condicaoEleitoral);
            deputadoFederal.GabineteAndar.Should().Be(deputadoFederalResponseMock.ultimoStatus.gabinete.andar);
            deputadoFederal.GabineteEmail.Should().Be(deputadoFederalResponseMock.ultimoStatus.gabinete.email);
            deputadoFederal.GabineteNome.Should().Be(deputadoFederalResponseMock.ultimoStatus.gabinete.nome);
            deputadoFederal.GabinetePredio.Should().Be(deputadoFederalResponseMock.ultimoStatus.gabinete.predio);
            deputadoFederal.GabineteSala.Should().Be(deputadoFederalResponseMock.ultimoStatus.gabinete.sala);
            deputadoFederal.GabineteTelefone.Should().Be(deputadoFederalResponseMock.ultimoStatus.gabinete.telefone);
        }

        [Fact]
        public void Update()
        {
            var deputadoFederalResponseMock = deputadoFederalResponseBuilder.Build();
            var deputadoFederal = deputadoFederalBuilder.Build();
            var cpf = deputadoFederal.CPF;
            deputadoFederal.Update((dynamic)deputadoFederalResponseMock);

            deputadoFederal.CPF.Should().Be(cpf);
            deputadoFederal.OrigemId.Should().Be(deputadoFederalResponseMock.id);
            deputadoFederal.URLWebsite.Should().Be(deputadoFederalResponseMock.urlWebsite);
            deputadoFederal.URLFoto.Should().Be(deputadoFederalResponseMock.ultimoStatus.urlFoto);
            deputadoFederal.CondicaoEleitoral.Should().Be(deputadoFederalResponseMock.ultimoStatus.condicaoEleitoral);
            deputadoFederal.GabineteAndar.Should().Be(deputadoFederalResponseMock.ultimoStatus.gabinete.andar);
            deputadoFederal.GabineteEmail.Should().Be(deputadoFederalResponseMock.ultimoStatus.gabinete.email);
            deputadoFederal.GabineteNome.Should().Be(deputadoFederalResponseMock.ultimoStatus.gabinete.nome);
            deputadoFederal.GabinetePredio.Should().Be(deputadoFederalResponseMock.ultimoStatus.gabinete.predio);
            deputadoFederal.GabineteSala.Should().Be(deputadoFederalResponseMock.ultimoStatus.gabinete.sala);
            deputadoFederal.GabineteTelefone.Should().Be(deputadoFederalResponseMock.ultimoStatus.gabinete.telefone);

        }

        [Fact]
        public void HasChange_False()
        {
            var deputadoFederalResponseMock = deputadoFederalResponseBuilder.Build();
            var deputadoFederal = new DeputadoFederal(deputadoFederalResponseMock);
            deputadoFederal.HasChange(deputadoFederalResponseMock).Should().BeFalse();
        }

        [Fact]
        public void HasChange_True()
        {
            var deputadoFederalResponseMock = deputadoFederalResponseBuilder.Build();
            var deputadoFederal = new DeputadoFederal(deputadoFederalResponseMock);
            deputadoFederalResponseMock.ultimoStatus.urlFoto = "teste";
            deputadoFederal.HasChange(deputadoFederalResponseMock).Should().BeTrue();
            deputadoFederalResponseMock.ultimoStatus.urlFoto = deputadoFederal.URLFoto;
            deputadoFederal.HasChange(deputadoFederalResponseMock).Should().BeFalse();
        }

        [Fact]
        public void AddNotaFiscalPeriodo_NotExist()
        {
            var deputadoFederal = deputadoFederalBuilder.Build();
            deputadoFederal.AddNotaFiscalPeriodosIfNotExist(2019, 11);
            deputadoFederal.NotasFiscaisPeriodos.Should().HaveCount(1);
            deputadoFederal.NotasFiscaisPeriodos.First().DeputadoFederal.Should().Be(deputadoFederal);
            deputadoFederal.NotasFiscaisPeriodos.First().DeputadoFederalId.Should().Be(deputadoFederal.Id);
            deputadoFederal.NotasFiscaisPeriodos.First().Ano.Should().Be(2019);
            deputadoFederal.NotasFiscaisPeriodos.First().Mes.Should().Be(11);   
        }

        [Fact]
        public void AddNotaFiscalPeriodo_Exist()
        {
            var deputadoFederal = deputadoFederalBuilder.Build();
            deputadoFederal.AddNotaFiscalPeriodosIfNotExist(2019, 11);
            deputadoFederal.NotasFiscaisPeriodos.Should().HaveCount(1);
            deputadoFederal.AddNotaFiscalPeriodosIfNotExist(2019, 11);
            deputadoFederal.NotasFiscaisPeriodos.Should().HaveCount(1);
            deputadoFederal.NotasFiscaisPeriodos.First().DeputadoFederal.Should().Be(deputadoFederal);
            deputadoFederal.NotasFiscaisPeriodos.First().DeputadoFederalId.Should().Be(deputadoFederal.Id);
            deputadoFederal.NotasFiscaisPeriodos.First().Ano.Should().Be(2019);
            deputadoFederal.NotasFiscaisPeriodos.First().Mes.Should().Be(11);   
        }
    }
}