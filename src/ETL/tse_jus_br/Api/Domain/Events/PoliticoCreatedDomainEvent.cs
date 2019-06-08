using System;
using MediatR;

namespace DeOlho.ETL.tse_jus_br.Api.Domain.Events
{
    public class PoliticoCreatedDomainEvent : INotification
    {
        readonly DateTime DT_GERACAO;
        readonly DateTime HH_GERACAO;
        readonly int ANO_ELEICAO;
        readonly int CD_TIPO_ELEICAO;
        readonly string NM_TIPO_ELEICAO;
        readonly int NR_TURNO;
        readonly int CD_ELEICAO;
        readonly string DS_ELEICAO;
        readonly DateTime DT_ELEICAO;
        readonly string TP_ABRANGENCIA;
        readonly string SG_UF;
        readonly string SG_UE;
        readonly string NM_UE;
        readonly int CD_CARGO;
        readonly string DS_CARGO;
        readonly long SQ_CANDIDATO;
        readonly int NR_CANDIDATO;
        readonly string NM_CANDIDATO;
        readonly string NM_URNA_CANDIDATO;
        readonly string NM_SOCIAL_CANDIDATO;
        readonly long NR_CPF_CANDIDATO;
        readonly string NM_EMAIL;
        readonly int CD_SITUACAO_CANDIDATURA;
        readonly string DS_SITUACAO_CANDIDATURA;
        readonly int CD_DETALHE_SITUACAO_CAND;
        readonly string DS_DETALHE_SITUACAO_CAND;
        readonly string TP_AGREMIACAO;
        readonly int NR_PARTIDO;
        readonly string SG_PARTIDO;
        readonly string NM_PARTIDO;
        readonly long SQ_COLIGACAO;
        readonly string NM_COLIGACAO;
        readonly string DS_COMPOSICAO_COLIGACAO;
        readonly int CD_NACIONALIDADE;
        readonly string DS_NACIONALIDADE;
        readonly string SG_UF_NASCIMENTO;
        readonly int CD_MUNICIPIO_NASCIMENTO;
        readonly string NM_MUNICIPIO_NASCIMENTO;
        readonly DateTime DT_NASCIMENTO;
        readonly int NR_IDADE_DATA_POSSE;
        readonly long NR_TITULO_ELEITORAL_CANDIDATO;
        readonly int CD_GENERO;
        readonly string DS_GENERO;
        readonly int CD_GRAU_INSTRUCAO;
        readonly string DS_GRAU_INSTRUCAO;
        readonly int CD_ESTADO_CIVIL;
        readonly string DS_ESTADO_CIVIL;
        readonly int CD_COR_RACA;
        readonly string DS_COR_RACA;
        readonly int CD_OCUPACAO;
        readonly string DS_OCUPACAO;
        readonly int NR_DESPESA_MAX_CAMPANHA;
        readonly int CD_SIT_TOT_TURNO;
        readonly string DS_SIT_TOT_TURNO;
        readonly string ST_REELEICAO;
        readonly string ST_DECLARAR_BENS;
        readonly int NR_PROTOCOLO_CANDIDATURA;
        readonly long NR_PROCESSO;


        public Politico Politico { get; private set; }

        public PoliticoCreatedDomainEvent(
            DateTime dt_geracao,
            DateTime hh_geracao,
            int ano_eleicao,
            int cd_tipo_eleicao,
            string nm_tipo_eleicao,
            int nr_turno,
            int cd_eleicao,
            string ds_eleicao,
            DateTime dt_eleicao,
            string tp_abrangencia,
            string sg_uf,
            string sg_ue,
            string nm_ue,
            int cd_cargo,
            string ds_cargo,
            long sq_candidato,
            int nr_candidato,
            string nm_candidato,
            string nm_urna_candidato,
            string nm_social_candidato,
            long nr_cpf_candidato,
            string nm_email,
            int cd_situacao_candidatura,
            string ds_situacao_candidatura,
            int cd_detalhe_situacao_cand,
            string ds_detalhe_situacao_cand,
            string tp_agremiacao,
            int nr_partido,
            string sg_partido,
            string nm_partido,
            long sq_coligacao,
            string nm_coligacao,
            string ds_composicao_coligacao,
            int cd_nacionalidade,
            string ds_nacionalidade,
            string sg_uf_nascimento,
            int cd_municipio_nascimento,
            string nm_municipio_nascimento,
            DateTime dt_nascimento,
            int nr_idade_data_posse,
            long nr_titulo_eleitoral_candidato,
            int cd_genero,
            string ds_genero,
            int cd_grau_instrucao,
            string ds_grau_instrucao,
            int cd_estado_civil,
            string ds_estado_civil,
            int cd_cor_raca,
            string ds_cor_raca,
            int cd_ocupacao,
            string ds_ocupacao,
            int nr_despesa_max_campanha,
            int cd_sit_tot_turno,
            string ds_sit_tot_turno,
            string st_reeleicao,
            string st_declarar_bens,
            int nr_protocolo_candidatura,
            long nr_processo)
        {
            DT_GERACAO = dt_geracao;
            HH_GERACAO = hh_geracao;
            ANO_ELEICAO = ano_eleicao;
            CD_TIPO_ELEICAO = cd_tipo_eleicao;
            NM_TIPO_ELEICAO = nm_tipo_eleicao;
            NR_TURNO = nr_turno;
            CD_ELEICAO = cd_eleicao;
            DS_ELEICAO = ds_eleicao;
            DT_ELEICAO = dt_eleicao;
            TP_ABRANGENCIA = tp_abrangencia;
            SG_UF = sg_uf;
            SG_UE = sg_ue;
            NM_UE = nm_ue;
            CD_CARGO = cd_cargo;
            DS_CARGO = ds_cargo;
            SQ_CANDIDATO = sq_candidato;
            NR_CANDIDATO = nr_candidato;
            NM_CANDIDATO = nm_candidato;
            NM_URNA_CANDIDATO = nm_urna_candidato;
            NM_SOCIAL_CANDIDATO = nm_social_candidato;
            NR_CPF_CANDIDATO = nr_cpf_candidato;
            NM_EMAIL = nm_email;
            CD_SITUACAO_CANDIDATURA = cd_situacao_candidatura;
            DS_SITUACAO_CANDIDATURA = ds_situacao_candidatura;
            CD_DETALHE_SITUACAO_CAND = cd_detalhe_situacao_cand;
            DS_DETALHE_SITUACAO_CAND = ds_detalhe_situacao_cand;
            TP_AGREMIACAO = tp_agremiacao;
            NR_PARTIDO = nr_partido;
            SG_PARTIDO = sg_partido;
            NM_PARTIDO = nm_partido;
            SQ_COLIGACAO = sq_coligacao;
            NM_COLIGACAO = nm_coligacao;
            DS_COMPOSICAO_COLIGACAO = ds_composicao_coligacao;
            CD_NACIONALIDADE = cd_nacionalidade;
            DS_NACIONALIDADE = ds_nacionalidade;
            SG_UF_NASCIMENTO = sg_uf_nascimento;
            CD_MUNICIPIO_NASCIMENTO = cd_municipio_nascimento;
            NM_MUNICIPIO_NASCIMENTO = nm_municipio_nascimento;
            DT_NASCIMENTO = dt_nascimento;
            NR_IDADE_DATA_POSSE = nr_idade_data_posse;
            NR_TITULO_ELEITORAL_CANDIDATO = nr_titulo_eleitoral_candidato;
            CD_GENERO = cd_genero;
            DS_GENERO = ds_genero;
            CD_GRAU_INSTRUCAO = cd_grau_instrucao;
            DS_GRAU_INSTRUCAO = ds_grau_instrucao;
            CD_ESTADO_CIVIL = cd_estado_civil;
            DS_ESTADO_CIVIL = ds_estado_civil;
            CD_COR_RACA = cd_cor_raca;
            DS_COR_RACA = ds_cor_raca;
            CD_OCUPACAO = cd_ocupacao;
            DS_OCUPACAO = ds_ocupacao;
            NR_DESPESA_MAX_CAMPANHA = nr_despesa_max_campanha;
            CD_SIT_TOT_TURNO = cd_sit_tot_turno;
            DS_SIT_TOT_TURNO = ds_sit_tot_turno;
            ST_REELEICAO = st_reeleicao;
            ST_DECLARAR_BENS = st_declarar_bens;
            NR_PROTOCOLO_CANDIDATURA = nr_protocolo_candidatura;
            NR_PROCESSO = nr_processo;
        }
    }
}