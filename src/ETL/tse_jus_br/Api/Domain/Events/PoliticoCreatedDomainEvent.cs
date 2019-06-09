using System;
using MediatR;

namespace DeOlho.ETL.tse_jus_br.Api.Domain.Events
{
    public class PoliticoCreatedDomainEvent : INotification
    {
        public DateTime DT_GERACAO { get; private set; }
        public DateTime HH_GERACAO { get; private set; }
        public int ANO_ELEICAO { get; private set; }
        public int CD_TIPO_ELEICAO { get; private set; }
        public string NM_TIPO_ELEICAO { get; private set; }
        public int NR_TURNO { get; private set; }
        public int CD_ELEICAO { get; private set; }
        public string DS_ELEICAO { get; private set; }
        public DateTime DT_ELEICAO { get; private set; }
        public string TP_ABRANGENCIA { get; private set; }
        public string SG_UF { get; private set; }
        public string SG_UE { get; private set; }
        public string NM_UE { get; private set; }
        public int CD_CARGO { get; private set; }
        public string DS_CARGO { get; private set; }
        public long SQ_CANDIDATO { get; private set; }
        public int NR_CANDIDATO { get; private set; }
        public string NM_CANDIDATO { get; private set; }
        public string NM_URNA_CANDIDATO { get; private set; }
        public string NM_SOCIAL_CANDIDATO { get; private set; }
        public long NR_CPF_CANDIDATO { get; private set; }
        public string NM_EMAIL { get; private set; }
        public int CD_SITUACAO_CANDIDATURA { get; private set; }
        public string DS_SITUACAO_CANDIDATURA { get; private set; }
        public int CD_DETALHE_SITUACAO_CAND { get; private set; }
        public string DS_DETALHE_SITUACAO_CAND { get; private set; }
        public string TP_AGREMIACAO { get; private set; }
        public int NR_PARTIDO { get; private set; }
        public string SG_PARTIDO { get; private set; }
        public string NM_PARTIDO { get; private set; }
        public long SQ_COLIGACAO { get; private set; }
        public string NM_COLIGACAO { get; private set; }
        public string DS_COMPOSICAO_COLIGACAO { get; private set; }
        public int CD_NACIONALIDADE { get; private set; }
        public string DS_NACIONALIDADE { get; private set; }
        public string SG_UF_NASCIMENTO { get; private set; }
        public int CD_MUNICIPIO_NASCIMENTO { get; private set; }
        public string NM_MUNICIPIO_NASCIMENTO { get; private set; }
        public DateTime DT_NASCIMENTO { get; private set; }
        public int NR_IDADE_DATA_POSSE { get; private set; }
        public long NR_TITULO_ELEITORAL_CANDIDATO { get; private set; }
        public int CD_GENERO { get; private set; }
        public string DS_GENERO { get; private set; }
        public int CD_GRAU_INSTRUCAO { get; private set; }
        public string DS_GRAU_INSTRUCAO { get; private set; }
        public int CD_ESTADO_CIVIL { get; private set; }
        public string DS_ESTADO_CIVIL { get; private set; }
        public int CD_COR_RACA { get; private set; }
        public string DS_COR_RACA { get; private set; }
        public int CD_OCUPACAO { get; private set; }
        public string DS_OCUPACAO { get; private set; }
        public int NR_DESPESA_MAX_CAMPANHA { get; private set; }
        public int CD_SIT_TOT_TURNO { get; private set; }
        public string DS_SIT_TOT_TURNO { get; private set; }
        public string ST_REELEICAO { get; private set; }
        public string ST_DECLARAR_BENS { get; private set; }
        public int NR_PROTOCOLO_CANDIDATURA { get; private set; }
        public long NR_PROCESSO { get; private set; }

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