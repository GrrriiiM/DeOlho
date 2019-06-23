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
    public class NotaFiscalPeriodoTest
    {
        private ISingleObjectBuilder<DeputadoFederal> deputadoFederalBuilder;
        private ISingleObjectBuilder<NotaFiscal> notaFiscalBuilder;
        private ISingleObjectBuilder<NotaFiscalResponse> notaFiscalResponseBuilder;

        private NotaFiscalPeriodo notaFiscalPeriodoBuilder() => new NotaFiscalPeriodo(deputadoFederalBuilder.Build(), 2019, 11);

        public NotaFiscalPeriodoTest()
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
            builderSettings.SetPropertyNamerFor<NotaFiscal>(namer);
            builderSettings.SetPropertyNamerFor<NotaFiscalResponse>(namer);
            
            
            deputadoFederalBuilder = builder.CreateNew<DeputadoFederal>();
            notaFiscalBuilder = builder.CreateNew<NotaFiscal>();
            notaFiscalResponseBuilder = builder.CreateNew<NotaFiscalResponse>()
                .With(_ => _.ano = 2019)
                .With(_ => _.mes = 11);
        }

        [Fact]
        public void AddNotaFiscalOrUpdateIfExist_NotExist()
        {
            var notaFiscalPeriodo = notaFiscalPeriodoBuilder();
            var notaFiscalResponse = notaFiscalResponseBuilder.Build();
            notaFiscalPeriodo.AddNotaFiscalOrUpdateIfExist(notaFiscalResponse);
            notaFiscalPeriodo.NotasFiscais.Should().HaveCount(1);
            var notaFiscal = notaFiscalPeriodo.NotasFiscais.First();
            notaFiscal.Periodo.Ano.Should().Be(notaFiscalResponse.ano);
            notaFiscal.Periodo.Mes.Should().Be(notaFiscalResponse.mes);
            notaFiscal.CodDocumento.Should().Be(notaFiscalResponse.codDocumento);
            notaFiscal.CnpjCpfFornecedor.Should().Be(notaFiscalResponse.cnpjCpfFornecedor);
            notaFiscal.CodLote.Should().Be(notaFiscalResponse.codLote);
            notaFiscal.CodTipoDocumento.Should().Be(notaFiscalResponse.codTipoDocumento);
            notaFiscal.DataDocumento.Should().Be(notaFiscalResponse.dataDocumento);
            notaFiscal.NomeFornecedor.Should().Be(notaFiscalResponse.nomeFornecedor);
            notaFiscal.NumDocumento.Should().Be(notaFiscalResponse.numDocumento);
            notaFiscal.NumRessarcimento.Should().Be(notaFiscalResponse.numRessarcimento);
            notaFiscal.Parcela.Should().Be(notaFiscalResponse.parcela);
            notaFiscal.TipoDespesa.Should().Be(notaFiscalResponse.tipoDespesa);
            notaFiscal.TipoDocumento.Should().Be(notaFiscalResponse.tipoDocumento);
            notaFiscal.URLDocumento.Should().Be(notaFiscalResponse.urlDocumento);
            notaFiscal.ValorDocumento.Should().Be(notaFiscalResponse.valorDocumento);
            notaFiscal.ValorGlosa.Should().Be(notaFiscalResponse.valorGlosa);
            notaFiscal.ValorLiquido.Should().Be(notaFiscalResponse.valorLiquido);
        }

        [Fact]
        public void AddNotaFiscalOrUpdateIfExist_Exist_NotChange()
        {
            var notaFiscalPeriodo = notaFiscalPeriodoBuilder();
            var notaFiscalResponse = notaFiscalResponseBuilder.Build();
            notaFiscalPeriodo.AddNotaFiscalOrUpdateIfExist(notaFiscalResponse);
            notaFiscalPeriodo.NotasFiscais.Should().HaveCount(1);
            notaFiscalPeriodo.AddNotaFiscalOrUpdateIfExist(notaFiscalResponse);
            notaFiscalPeriodo.NotasFiscais.Should().HaveCount(1);
        }

        [Fact]
        public void AddNotaFiscalOrUpdateIfExist_Exist_HasChange()
        {
            var notaFiscalPeriodo = notaFiscalPeriodoBuilder();
            var notaFiscalResponse = notaFiscalResponseBuilder.Build();
            notaFiscalPeriodo.AddNotaFiscalOrUpdateIfExist(notaFiscalResponse);
            notaFiscalPeriodo.NotasFiscais.Should().HaveCount(1);
            notaFiscalResponse.valorGlosa = 1234.45M;
            notaFiscalPeriodo.AddNotaFiscalOrUpdateIfExist(notaFiscalResponse);
            notaFiscalPeriodo.NotasFiscais.Should().HaveCount(1);
            var notaFiscal = notaFiscalPeriodo.NotasFiscais.First();
            notaFiscal.Periodo.Ano.Should().Be(notaFiscalResponse.ano);
            notaFiscal.Periodo.Mes.Should().Be(notaFiscalResponse.mes);
            notaFiscal.CodDocumento.Should().Be(notaFiscalResponse.codDocumento);
            notaFiscal.CnpjCpfFornecedor.Should().Be(notaFiscalResponse.cnpjCpfFornecedor);
            notaFiscal.CodLote.Should().Be(notaFiscalResponse.codLote);
            notaFiscal.CodTipoDocumento.Should().Be(notaFiscalResponse.codTipoDocumento);
            notaFiscal.DataDocumento.Should().Be(notaFiscalResponse.dataDocumento);
            notaFiscal.NomeFornecedor.Should().Be(notaFiscalResponse.nomeFornecedor);
            notaFiscal.NumDocumento.Should().Be(notaFiscalResponse.numDocumento);
            notaFiscal.NumRessarcimento.Should().Be(notaFiscalResponse.numRessarcimento);
            notaFiscal.Parcela.Should().Be(notaFiscalResponse.parcela);
            notaFiscal.TipoDespesa.Should().Be(notaFiscalResponse.tipoDespesa);
            notaFiscal.TipoDocumento.Should().Be(notaFiscalResponse.tipoDocumento);
            notaFiscal.URLDocumento.Should().Be(notaFiscalResponse.urlDocumento);
            notaFiscal.ValorDocumento.Should().Be(notaFiscalResponse.valorDocumento);
            notaFiscal.ValorGlosa.Should().Be(notaFiscalResponse.valorGlosa);
            notaFiscal.ValorLiquido.Should().Be(notaFiscalResponse.valorLiquido);
        }

    }
}