using System;
using System.Linq;
using DeOlho.ETL.tse_jus_br.Api.Domain;
using DeOlho.ETL.tse_jus_br.Api.Domain.Events;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using FluentAssertions;
using Moq;
using Xunit;

namespace UnitTest.Domain
{
    public class PoliticoTest
    {

        Politico politicoSetup;

        public PoliticoTest()
        {
            if (BuilderSetup.GetPropertyNamerFor<Politico>() == null)
            {
                var namer = new RandomValuePropertyNamer(
                    new RandomGenerator(), 
                    new ReflectionUtil(), 
                    true, 
                    DateTime.Now, 
                    DateTime.Now.AddDays(10), 
                    true, 
                    new BuilderSettings());

                BuilderSetup.SetPropertyNamerFor<Politico>(namer);
            }

            politicoSetup  = Builder<Politico>.CreateNew().Build();    
        }

        [Fact]
        public void Constructor()
        {
            var politico = new Politico(
                politicoSetup.DT_GERACAO,
                politicoSetup.HH_GERACAO,
                politicoSetup.ANO_ELEICAO,
                politicoSetup.CD_TIPO_ELEICAO,
                politicoSetup.NM_TIPO_ELEICAO,
                politicoSetup.NR_TURNO,
                politicoSetup.CD_ELEICAO,
                politicoSetup.DS_ELEICAO,
                politicoSetup.DT_ELEICAO,
                politicoSetup.TP_ABRANGENCIA,
                politicoSetup.SG_UF,
                politicoSetup.SG_UE,
                politicoSetup.NM_UE,
                politicoSetup.CD_CARGO,
                politicoSetup.DS_CARGO,
                politicoSetup.SQ_CANDIDATO,
                politicoSetup.NR_CANDIDATO,
                politicoSetup.NM_CANDIDATO,
                politicoSetup.NM_URNA_CANDIDATO,
                politicoSetup.NM_SOCIAL_CANDIDATO,
                politicoSetup.NR_CPF_CANDIDATO,
                politicoSetup.NM_EMAIL,
                politicoSetup.CD_SITUACAO_CANDIDATURA,
                politicoSetup.DS_SITUACAO_CANDIDATURA,
                politicoSetup.CD_DETALHE_SITUACAO_CAND,
                politicoSetup.DS_DETALHE_SITUACAO_CAND,
                politicoSetup.TP_AGREMIACAO,
                politicoSetup.NR_PARTIDO,
                politicoSetup.SG_PARTIDO,
                politicoSetup.NM_PARTIDO,
                politicoSetup.SQ_COLIGACAO,
                politicoSetup.NM_COLIGACAO,
                politicoSetup.DS_COMPOSICAO_COLIGACAO,
                politicoSetup.CD_NACIONALIDADE,
                politicoSetup.DS_NACIONALIDADE,
                politicoSetup.SG_UF_NASCIMENTO,
                politicoSetup.CD_MUNICIPIO_NASCIMENTO,
                politicoSetup.NM_MUNICIPIO_NASCIMENTO,
                politicoSetup.DT_NASCIMENTO,
                politicoSetup.NR_IDADE_DATA_POSSE,
                politicoSetup.NR_TITULO_ELEITORAL_CANDIDATO,
                politicoSetup.CD_GENERO,
                politicoSetup.DS_GENERO,
                politicoSetup.CD_GRAU_INSTRUCAO,
                politicoSetup.DS_GRAU_INSTRUCAO,
                politicoSetup.CD_ESTADO_CIVIL,
                politicoSetup.DS_ESTADO_CIVIL,
                politicoSetup.CD_COR_RACA,
                politicoSetup.DS_COR_RACA,
                politicoSetup.CD_OCUPACAO,
                politicoSetup.DS_OCUPACAO,
                politicoSetup.NR_DESPESA_MAX_CAMPANHA,
                politicoSetup.CD_SIT_TOT_TURNO,
                politicoSetup.DS_SIT_TOT_TURNO,
                politicoSetup.ST_REELEICAO,
                politicoSetup.ST_DECLARAR_BENS,
                politicoSetup.NR_PROTOCOLO_CANDIDATURA,
                politicoSetup.NR_PROCESSO
            );

            politico.DT_GERACAO.Should().Be(politicoSetup.DT_GERACAO);
            politico.HH_GERACAO.Should().Be(politicoSetup.HH_GERACAO);
            politico.ANO_ELEICAO.Should().Be(politicoSetup.ANO_ELEICAO);
            politico.CD_TIPO_ELEICAO.Should().Be(politicoSetup.CD_TIPO_ELEICAO);
            politico.NM_TIPO_ELEICAO.Should().Be(politicoSetup.NM_TIPO_ELEICAO);
            politico.NR_TURNO.Should().Be(politicoSetup.NR_TURNO);
            politico.CD_ELEICAO.Should().Be(politicoSetup.CD_ELEICAO);
            politico.DS_ELEICAO.Should().Be(politicoSetup.DS_ELEICAO);
            politico.DT_ELEICAO.Should().Be(politicoSetup.DT_ELEICAO);
            politico.TP_ABRANGENCIA.Should().Be(politicoSetup.TP_ABRANGENCIA);
            politico.SG_UF.Should().Be(politicoSetup.SG_UF);
            politico.SG_UE.Should().Be(politicoSetup.SG_UE);
            politico.NM_UE.Should().Be(politicoSetup.NM_UE);
            politico.CD_CARGO.Should().Be(politicoSetup.CD_CARGO);
            politico.DS_CARGO.Should().Be(politicoSetup.DS_CARGO);
            politico.SQ_CANDIDATO.Should().Be(politicoSetup.SQ_CANDIDATO);
            politico.NR_CANDIDATO.Should().Be(politicoSetup.NR_CANDIDATO);
            politico.NM_CANDIDATO.Should().Be(politicoSetup.NM_CANDIDATO);
            politico.NM_URNA_CANDIDATO.Should().Be(politicoSetup.NM_URNA_CANDIDATO);
            politico.NM_SOCIAL_CANDIDATO.Should().Be(politicoSetup.NM_SOCIAL_CANDIDATO);
            politico.NR_CPF_CANDIDATO.Should().Be(politicoSetup.NR_CPF_CANDIDATO);
            politico.NM_EMAIL.Should().Be(politicoSetup.NM_EMAIL);
            politico.CD_SITUACAO_CANDIDATURA.Should().Be(politicoSetup.CD_SITUACAO_CANDIDATURA);
            politico.DS_SITUACAO_CANDIDATURA.Should().Be(politicoSetup.DS_SITUACAO_CANDIDATURA);
            politico.CD_DETALHE_SITUACAO_CAND.Should().Be(politicoSetup.CD_DETALHE_SITUACAO_CAND);
            politico.DS_DETALHE_SITUACAO_CAND.Should().Be(politicoSetup.DS_DETALHE_SITUACAO_CAND);
            politico.TP_AGREMIACAO.Should().Be(politicoSetup.TP_AGREMIACAO);
            politico.NR_PARTIDO.Should().Be(politicoSetup.NR_PARTIDO);
            politico.SG_PARTIDO.Should().Be(politicoSetup.SG_PARTIDO);
            politico.NM_PARTIDO.Should().Be(politicoSetup.NM_PARTIDO);
            politico.SQ_COLIGACAO.Should().Be(politicoSetup.SQ_COLIGACAO);
            politico.NM_COLIGACAO.Should().Be(politicoSetup.NM_COLIGACAO);
            politico.DS_COMPOSICAO_COLIGACAO.Should().Be(politicoSetup.DS_COMPOSICAO_COLIGACAO);
            politico.CD_NACIONALIDADE.Should().Be(politicoSetup.CD_NACIONALIDADE);
            politico.DS_NACIONALIDADE.Should().Be(politicoSetup.DS_NACIONALIDADE);
            politico.SG_UF_NASCIMENTO.Should().Be(politicoSetup.SG_UF_NASCIMENTO);
            politico.CD_MUNICIPIO_NASCIMENTO.Should().Be(politicoSetup.CD_MUNICIPIO_NASCIMENTO);
            politico.NM_MUNICIPIO_NASCIMENTO.Should().Be(politicoSetup.NM_MUNICIPIO_NASCIMENTO);
            politico.DT_NASCIMENTO.Should().Be(politicoSetup.DT_NASCIMENTO);
            politico.NR_IDADE_DATA_POSSE.Should().Be(politicoSetup.NR_IDADE_DATA_POSSE);
            politico.NR_TITULO_ELEITORAL_CANDIDATO.Should().Be(politicoSetup.NR_TITULO_ELEITORAL_CANDIDATO);
            politico.CD_GENERO.Should().Be(politicoSetup.CD_GENERO);
            politico.DS_GENERO.Should().Be(politicoSetup.DS_GENERO);
            politico.CD_GRAU_INSTRUCAO.Should().Be(politicoSetup.CD_GRAU_INSTRUCAO);
            politico.DS_GRAU_INSTRUCAO.Should().Be(politicoSetup.DS_GRAU_INSTRUCAO);
            politico.CD_ESTADO_CIVIL.Should().Be(politicoSetup.CD_ESTADO_CIVIL);
            politico.DS_ESTADO_CIVIL.Should().Be(politicoSetup.DS_ESTADO_CIVIL);
            politico.CD_COR_RACA.Should().Be(politicoSetup.CD_COR_RACA);
            politico.DS_COR_RACA.Should().Be(politicoSetup.DS_COR_RACA);
            politico.CD_OCUPACAO.Should().Be(politicoSetup.CD_OCUPACAO);
            politico.DS_OCUPACAO.Should().Be(politicoSetup.DS_OCUPACAO);
            politico.NR_DESPESA_MAX_CAMPANHA.Should().Be(politicoSetup.NR_DESPESA_MAX_CAMPANHA);
            politico.CD_SIT_TOT_TURNO.Should().Be(politicoSetup.CD_SIT_TOT_TURNO);
            politico.DS_SIT_TOT_TURNO.Should().Be(politicoSetup.DS_SIT_TOT_TURNO);
            politico.ST_REELEICAO.Should().Be(politicoSetup.ST_REELEICAO);
            politico.ST_DECLARAR_BENS.Should().Be(politicoSetup.ST_DECLARAR_BENS);
            politico.NR_PROTOCOLO_CANDIDATURA.Should().Be(politicoSetup.NR_PROTOCOLO_CANDIDATURA);
            politico.NR_PROCESSO.Should().Be(politicoSetup.NR_PROCESSO);

            var domainEvents = politico.GetDomainEvents().OfType<PoliticoCreatedDomainEvent>();

            domainEvents.Should().HaveCount(1);
            var domainEvent = domainEvents.FirstOrDefault();

            domainEvent.DT_GERACAO.Should().Be(politicoSetup.DT_GERACAO);
            domainEvent.HH_GERACAO.Should().Be(politicoSetup.HH_GERACAO);
            domainEvent.ANO_ELEICAO.Should().Be(politicoSetup.ANO_ELEICAO);
            domainEvent.CD_TIPO_ELEICAO.Should().Be(politicoSetup.CD_TIPO_ELEICAO);
            domainEvent.NM_TIPO_ELEICAO.Should().Be(politicoSetup.NM_TIPO_ELEICAO);
            domainEvent.NR_TURNO.Should().Be(politicoSetup.NR_TURNO);
            domainEvent.CD_ELEICAO.Should().Be(politicoSetup.CD_ELEICAO);
            domainEvent.DS_ELEICAO.Should().Be(politicoSetup.DS_ELEICAO);
            domainEvent.DT_ELEICAO.Should().Be(politicoSetup.DT_ELEICAO);
            domainEvent.TP_ABRANGENCIA.Should().Be(politicoSetup.TP_ABRANGENCIA);
            domainEvent.SG_UF.Should().Be(politicoSetup.SG_UF);
            domainEvent.SG_UE.Should().Be(politicoSetup.SG_UE);
            domainEvent.NM_UE.Should().Be(politicoSetup.NM_UE);
            domainEvent.CD_CARGO.Should().Be(politicoSetup.CD_CARGO);
            domainEvent.DS_CARGO.Should().Be(politicoSetup.DS_CARGO);
            domainEvent.SQ_CANDIDATO.Should().Be(politicoSetup.SQ_CANDIDATO);
            domainEvent.NR_CANDIDATO.Should().Be(politicoSetup.NR_CANDIDATO);
            domainEvent.NM_CANDIDATO.Should().Be(politicoSetup.NM_CANDIDATO);
            domainEvent.NM_URNA_CANDIDATO.Should().Be(politicoSetup.NM_URNA_CANDIDATO);
            domainEvent.NM_SOCIAL_CANDIDATO.Should().Be(politicoSetup.NM_SOCIAL_CANDIDATO);
            domainEvent.NR_CPF_CANDIDATO.Should().Be(politicoSetup.NR_CPF_CANDIDATO);
            domainEvent.NM_EMAIL.Should().Be(politicoSetup.NM_EMAIL);
            domainEvent.CD_SITUACAO_CANDIDATURA.Should().Be(politicoSetup.CD_SITUACAO_CANDIDATURA);
            domainEvent.DS_SITUACAO_CANDIDATURA.Should().Be(politicoSetup.DS_SITUACAO_CANDIDATURA);
            domainEvent.CD_DETALHE_SITUACAO_CAND.Should().Be(politicoSetup.CD_DETALHE_SITUACAO_CAND);
            domainEvent.DS_DETALHE_SITUACAO_CAND.Should().Be(politicoSetup.DS_DETALHE_SITUACAO_CAND);
            domainEvent.TP_AGREMIACAO.Should().Be(politicoSetup.TP_AGREMIACAO);
            domainEvent.NR_PARTIDO.Should().Be(politicoSetup.NR_PARTIDO);
            domainEvent.SG_PARTIDO.Should().Be(politicoSetup.SG_PARTIDO);
            domainEvent.NM_PARTIDO.Should().Be(politicoSetup.NM_PARTIDO);
            domainEvent.SQ_COLIGACAO.Should().Be(politicoSetup.SQ_COLIGACAO);
            domainEvent.NM_COLIGACAO.Should().Be(politicoSetup.NM_COLIGACAO);
            domainEvent.DS_COMPOSICAO_COLIGACAO.Should().Be(politicoSetup.DS_COMPOSICAO_COLIGACAO);
            domainEvent.CD_NACIONALIDADE.Should().Be(politicoSetup.CD_NACIONALIDADE);
            domainEvent.DS_NACIONALIDADE.Should().Be(politicoSetup.DS_NACIONALIDADE);
            domainEvent.SG_UF_NASCIMENTO.Should().Be(politicoSetup.SG_UF_NASCIMENTO);
            domainEvent.CD_MUNICIPIO_NASCIMENTO.Should().Be(politicoSetup.CD_MUNICIPIO_NASCIMENTO);
            domainEvent.NM_MUNICIPIO_NASCIMENTO.Should().Be(politicoSetup.NM_MUNICIPIO_NASCIMENTO);
            domainEvent.DT_NASCIMENTO.Should().Be(politicoSetup.DT_NASCIMENTO);
            domainEvent.NR_IDADE_DATA_POSSE.Should().Be(politicoSetup.NR_IDADE_DATA_POSSE);
            domainEvent.NR_TITULO_ELEITORAL_CANDIDATO.Should().Be(politicoSetup.NR_TITULO_ELEITORAL_CANDIDATO);
            domainEvent.CD_GENERO.Should().Be(politicoSetup.CD_GENERO);
            domainEvent.DS_GENERO.Should().Be(politicoSetup.DS_GENERO);
            domainEvent.CD_GRAU_INSTRUCAO.Should().Be(politicoSetup.CD_GRAU_INSTRUCAO);
            domainEvent.DS_GRAU_INSTRUCAO.Should().Be(politicoSetup.DS_GRAU_INSTRUCAO);
            domainEvent.CD_ESTADO_CIVIL.Should().Be(politicoSetup.CD_ESTADO_CIVIL);
            domainEvent.DS_ESTADO_CIVIL.Should().Be(politicoSetup.DS_ESTADO_CIVIL);
            domainEvent.CD_COR_RACA.Should().Be(politicoSetup.CD_COR_RACA);
            domainEvent.DS_COR_RACA.Should().Be(politicoSetup.DS_COR_RACA);
            domainEvent.CD_OCUPACAO.Should().Be(politicoSetup.CD_OCUPACAO);
            domainEvent.DS_OCUPACAO.Should().Be(politicoSetup.DS_OCUPACAO);
            domainEvent.NR_DESPESA_MAX_CAMPANHA.Should().Be(politicoSetup.NR_DESPESA_MAX_CAMPANHA);
            domainEvent.CD_SIT_TOT_TURNO.Should().Be(politicoSetup.CD_SIT_TOT_TURNO);
            domainEvent.DS_SIT_TOT_TURNO.Should().Be(politicoSetup.DS_SIT_TOT_TURNO);
            domainEvent.ST_REELEICAO.Should().Be(politicoSetup.ST_REELEICAO);
            domainEvent.ST_DECLARAR_BENS.Should().Be(politicoSetup.ST_DECLARAR_BENS);
            domainEvent.NR_PROTOCOLO_CANDIDATURA.Should().Be(politicoSetup.NR_PROTOCOLO_CANDIDATURA);
            domainEvent.NR_PROCESSO.Should().Be(politicoSetup.NR_PROCESSO);
            
        }

        [Fact]
        public void IsEqual()
        {
            var politico = new Politico(
                politicoSetup.DT_GERACAO,
                politicoSetup.HH_GERACAO,
                politicoSetup.ANO_ELEICAO,
                politicoSetup.CD_TIPO_ELEICAO,
                politicoSetup.NM_TIPO_ELEICAO,
                politicoSetup.NR_TURNO,
                politicoSetup.CD_ELEICAO,
                politicoSetup.DS_ELEICAO,
                politicoSetup.DT_ELEICAO,
                politicoSetup.TP_ABRANGENCIA,
                politicoSetup.SG_UF,
                politicoSetup.SG_UE,
                politicoSetup.NM_UE,
                politicoSetup.CD_CARGO,
                politicoSetup.DS_CARGO,
                politicoSetup.SQ_CANDIDATO,
                politicoSetup.NR_CANDIDATO,
                politicoSetup.NM_CANDIDATO,
                politicoSetup.NM_URNA_CANDIDATO,
                politicoSetup.NM_SOCIAL_CANDIDATO,
                politicoSetup.NR_CPF_CANDIDATO,
                politicoSetup.NM_EMAIL,
                politicoSetup.CD_SITUACAO_CANDIDATURA,
                politicoSetup.DS_SITUACAO_CANDIDATURA,
                politicoSetup.CD_DETALHE_SITUACAO_CAND,
                politicoSetup.DS_DETALHE_SITUACAO_CAND,
                politicoSetup.TP_AGREMIACAO,
                politicoSetup.NR_PARTIDO,
                politicoSetup.SG_PARTIDO,
                politicoSetup.NM_PARTIDO,
                politicoSetup.SQ_COLIGACAO,
                politicoSetup.NM_COLIGACAO,
                politicoSetup.DS_COMPOSICAO_COLIGACAO,
                politicoSetup.CD_NACIONALIDADE,
                politicoSetup.DS_NACIONALIDADE,
                politicoSetup.SG_UF_NASCIMENTO,
                politicoSetup.CD_MUNICIPIO_NASCIMENTO,
                politicoSetup.NM_MUNICIPIO_NASCIMENTO,
                politicoSetup.DT_NASCIMENTO,
                politicoSetup.NR_IDADE_DATA_POSSE,
                politicoSetup.NR_TITULO_ELEITORAL_CANDIDATO,
                politicoSetup.CD_GENERO,
                politicoSetup.DS_GENERO,
                politicoSetup.CD_GRAU_INSTRUCAO,
                politicoSetup.DS_GRAU_INSTRUCAO,
                politicoSetup.CD_ESTADO_CIVIL,
                politicoSetup.DS_ESTADO_CIVIL,
                politicoSetup.CD_COR_RACA,
                politicoSetup.DS_COR_RACA,
                politicoSetup.CD_OCUPACAO,
                politicoSetup.DS_OCUPACAO,
                politicoSetup.NR_DESPESA_MAX_CAMPANHA,
                politicoSetup.CD_SIT_TOT_TURNO,
                politicoSetup.DS_SIT_TOT_TURNO,
                politicoSetup.ST_REELEICAO,
                politicoSetup.ST_DECLARAR_BENS,
                politicoSetup.NR_PROTOCOLO_CANDIDATURA,
                politicoSetup.NR_PROCESSO
            );

            politico.IsEqual(politicoSetup).Should().Be(true);

            politico.ANO_ELEICAO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.ANO_ELEICAO = politicoSetup.ANO_ELEICAO;
            politico.CD_TIPO_ELEICAO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.CD_TIPO_ELEICAO = politicoSetup.CD_TIPO_ELEICAO;
            politico.NM_TIPO_ELEICAO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NM_TIPO_ELEICAO = politicoSetup.NM_TIPO_ELEICAO;
            politico.NR_TURNO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NR_TURNO = politicoSetup.NR_TURNO;
            politico.CD_ELEICAO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.CD_ELEICAO = politicoSetup.CD_ELEICAO;
            politico.DS_ELEICAO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.DS_ELEICAO = politicoSetup.DS_ELEICAO;
            politico.DT_ELEICAO = politico.DT_ELEICAO.AddDays(1);
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.DT_ELEICAO = politicoSetup.DT_ELEICAO;
            politico.TP_ABRANGENCIA += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.TP_ABRANGENCIA = politicoSetup.TP_ABRANGENCIA;
            politico.SG_UF += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.SG_UF = politicoSetup.SG_UF;
            politico.SG_UE += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.SG_UE = politicoSetup.SG_UE;
            politico.NM_UE += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NM_UE = politicoSetup.NM_UE;
            politico.CD_CARGO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.CD_CARGO = politicoSetup.CD_CARGO;
            politico.DS_CARGO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.DS_CARGO = politicoSetup.DS_CARGO;
            politico.SQ_CANDIDATO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.SQ_CANDIDATO = politicoSetup.SQ_CANDIDATO;
            politico.NR_CANDIDATO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NR_CANDIDATO = politicoSetup.NR_CANDIDATO;
            politico.NM_CANDIDATO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NM_CANDIDATO = politicoSetup.NM_CANDIDATO;
            politico.NM_URNA_CANDIDATO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NM_URNA_CANDIDATO = politicoSetup.NM_URNA_CANDIDATO;
            politico.NM_SOCIAL_CANDIDATO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NM_SOCIAL_CANDIDATO = politicoSetup.NM_SOCIAL_CANDIDATO;
            politico.NR_CPF_CANDIDATO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NR_CPF_CANDIDATO = politicoSetup.NR_CPF_CANDIDATO;
            politico.NM_EMAIL += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NM_EMAIL = politicoSetup.NM_EMAIL;
            politico.CD_SITUACAO_CANDIDATURA += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.CD_SITUACAO_CANDIDATURA = politicoSetup.CD_SITUACAO_CANDIDATURA;
            politico.DS_SITUACAO_CANDIDATURA += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.DS_SITUACAO_CANDIDATURA = politicoSetup.DS_SITUACAO_CANDIDATURA;
            politico.CD_DETALHE_SITUACAO_CAND += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.CD_DETALHE_SITUACAO_CAND = politicoSetup.CD_DETALHE_SITUACAO_CAND;
            politico.DS_DETALHE_SITUACAO_CAND += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.DS_DETALHE_SITUACAO_CAND = politicoSetup.DS_DETALHE_SITUACAO_CAND;
            politico.TP_AGREMIACAO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.TP_AGREMIACAO = politicoSetup.TP_AGREMIACAO;
            politico.NR_PARTIDO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NR_PARTIDO = politicoSetup.NR_PARTIDO;
            politico.SG_PARTIDO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.SG_PARTIDO = politicoSetup.SG_PARTIDO;
            politico.NM_PARTIDO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NM_PARTIDO = politicoSetup.NM_PARTIDO;
            politico.SQ_COLIGACAO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.SQ_COLIGACAO = politicoSetup.SQ_COLIGACAO;
            politico.NM_COLIGACAO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NM_COLIGACAO = politicoSetup.NM_COLIGACAO;
            politico.DS_COMPOSICAO_COLIGACAO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.DS_COMPOSICAO_COLIGACAO = politicoSetup.DS_COMPOSICAO_COLIGACAO;
            politico.CD_NACIONALIDADE += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.CD_NACIONALIDADE = politicoSetup.CD_NACIONALIDADE;
            politico.DS_NACIONALIDADE += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.DS_NACIONALIDADE = politicoSetup.DS_NACIONALIDADE;
            politico.SG_UF_NASCIMENTO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.SG_UF_NASCIMENTO = politicoSetup.SG_UF_NASCIMENTO;
            politico.CD_MUNICIPIO_NASCIMENTO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.CD_MUNICIPIO_NASCIMENTO = politicoSetup.CD_MUNICIPIO_NASCIMENTO;
            politico.NM_MUNICIPIO_NASCIMENTO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NM_MUNICIPIO_NASCIMENTO = politicoSetup.NM_MUNICIPIO_NASCIMENTO;
            politico.DT_NASCIMENTO = politico.DT_NASCIMENTO.AddDays(1);
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.DT_NASCIMENTO = politicoSetup.DT_NASCIMENTO;
            politico.NR_IDADE_DATA_POSSE += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NR_IDADE_DATA_POSSE = politicoSetup.NR_IDADE_DATA_POSSE;
            politico.NR_TITULO_ELEITORAL_CANDIDATO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NR_TITULO_ELEITORAL_CANDIDATO = politicoSetup.NR_TITULO_ELEITORAL_CANDIDATO;
            politico.CD_GENERO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.CD_GENERO = politicoSetup.CD_GENERO;
            politico.DS_GENERO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.DS_GENERO = politicoSetup.DS_GENERO;
            politico.CD_GRAU_INSTRUCAO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.CD_GRAU_INSTRUCAO = politicoSetup.CD_GRAU_INSTRUCAO;
            politico.DS_GRAU_INSTRUCAO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.DS_GRAU_INSTRUCAO = politicoSetup.DS_GRAU_INSTRUCAO;
            politico.CD_ESTADO_CIVIL += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.CD_ESTADO_CIVIL = politicoSetup.CD_ESTADO_CIVIL;
            politico.DS_ESTADO_CIVIL += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.DS_ESTADO_CIVIL = politicoSetup.DS_ESTADO_CIVIL;
            politico.CD_COR_RACA += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.CD_COR_RACA = politicoSetup.CD_COR_RACA;
            politico.DS_COR_RACA += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.DS_COR_RACA = politicoSetup.DS_COR_RACA;
            politico.CD_OCUPACAO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.CD_OCUPACAO = politicoSetup.CD_OCUPACAO;
            politico.DS_OCUPACAO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.DS_OCUPACAO = politicoSetup.DS_OCUPACAO;
            politico.NR_DESPESA_MAX_CAMPANHA += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NR_DESPESA_MAX_CAMPANHA = politicoSetup.NR_DESPESA_MAX_CAMPANHA;
            politico.CD_SIT_TOT_TURNO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.CD_SIT_TOT_TURNO = politicoSetup.CD_SIT_TOT_TURNO;
            politico.DS_SIT_TOT_TURNO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.DS_SIT_TOT_TURNO = politicoSetup.DS_SIT_TOT_TURNO;
            politico.ST_REELEICAO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.ST_REELEICAO = politicoSetup.ST_REELEICAO;
            politico.ST_DECLARAR_BENS += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.ST_DECLARAR_BENS = politicoSetup.ST_DECLARAR_BENS;
            politico.NR_PROTOCOLO_CANDIDATURA += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NR_PROTOCOLO_CANDIDATURA = politicoSetup.NR_PROTOCOLO_CANDIDATURA;
            politico.NR_PROCESSO += 1;
            politico.IsEqual(politicoSetup).Should().Be(false);
            politico.NR_PROCESSO = politicoSetup.NR_PROCESSO;

            politico.IsEqual(politicoSetup).Should().Be(true);

        }

    }
}