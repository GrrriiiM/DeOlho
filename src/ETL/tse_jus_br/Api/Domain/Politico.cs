using System;
using DeOlho.ETL.tse_jus_br.Api.Domain.Events;
using DeOlho.ETL.tse_jus_br.Api.Domain.SeedWork;

namespace DeOlho.ETL.tse_jus_br.Api.Domain
{
    public class Politico : Entity
    {
        protected Politico() {}
        public Politico(
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

            AddDomainEvent(new PoliticoCreatedDomainEvent(
                DT_GERACAO,
                HH_GERACAO,
                ANO_ELEICAO,
                CD_TIPO_ELEICAO,
                NM_TIPO_ELEICAO,
                NR_TURNO,
                CD_ELEICAO,
                DS_ELEICAO,
                DT_ELEICAO,
                TP_ABRANGENCIA,
                SG_UF,
                SG_UE,
                NM_UE,
                CD_CARGO,
                DS_CARGO,
                SQ_CANDIDATO,
                NR_CANDIDATO,
                NM_CANDIDATO,
                NM_URNA_CANDIDATO,
                NM_SOCIAL_CANDIDATO,
                NR_CPF_CANDIDATO,
                NM_EMAIL,
                CD_SITUACAO_CANDIDATURA,
                DS_SITUACAO_CANDIDATURA,
                CD_DETALHE_SITUACAO_CAND,
                DS_DETALHE_SITUACAO_CAND,
                TP_AGREMIACAO,
                NR_PARTIDO,
                SG_PARTIDO,
                NM_PARTIDO,
                SQ_COLIGACAO,
                NM_COLIGACAO,
                DS_COMPOSICAO_COLIGACAO,
                CD_NACIONALIDADE,
                DS_NACIONALIDADE,
                SG_UF_NASCIMENTO,
                CD_MUNICIPIO_NASCIMENTO,
                NM_MUNICIPIO_NASCIMENTO,
                DT_NASCIMENTO,
                NR_IDADE_DATA_POSSE,
                NR_TITULO_ELEITORAL_CANDIDATO,
                CD_GENERO,
                DS_GENERO,
                CD_GRAU_INSTRUCAO,
                DS_GRAU_INSTRUCAO,
                CD_ESTADO_CIVIL,
                DS_ESTADO_CIVIL,
                CD_COR_RACA,
                DS_COR_RACA,
                CD_OCUPACAO,
                DS_OCUPACAO,
                NR_DESPESA_MAX_CAMPANHA,
                CD_SIT_TOT_TURNO,
                DS_SIT_TOT_TURNO,
                ST_REELEICAO,
                ST_DECLARAR_BENS,
                NR_PROTOCOLO_CANDIDATURA,
                NR_PROCESSO
            ));

        }

        public DateTime DT_GERACAO { get; set; }
        public DateTime HH_GERACAO { get; set; }
        public int ANO_ELEICAO { get; set; }
        public int CD_TIPO_ELEICAO { get; set; }
        public string NM_TIPO_ELEICAO { get; set; }
        public int NR_TURNO { get; set; }
        public int CD_ELEICAO { get; set; }
        public string DS_ELEICAO { get; set; }
        public DateTime DT_ELEICAO { get; set; }
        public string TP_ABRANGENCIA { get; set; }
        public string SG_UF { get; set; }
        public string SG_UE { get; set; }
        public string NM_UE { get; set; }
        public int CD_CARGO { get; set; }
        public string DS_CARGO { get; set; }
        public long SQ_CANDIDATO { get; set; }
        public int NR_CANDIDATO { get; set; }
        public string NM_CANDIDATO { get; set; }
        public string NM_URNA_CANDIDATO { get; set; }
        public string NM_SOCIAL_CANDIDATO { get; set; }
        public long NR_CPF_CANDIDATO { get; set; }
        public string NM_EMAIL { get; set; }
        public int CD_SITUACAO_CANDIDATURA { get; set; }
        public string DS_SITUACAO_CANDIDATURA { get; set; }
        public int CD_DETALHE_SITUACAO_CAND { get; set; }
        public string DS_DETALHE_SITUACAO_CAND { get; set; }
        public string TP_AGREMIACAO { get; set; }
        public int NR_PARTIDO { get; set; }
        public string SG_PARTIDO { get; set; }
        public string NM_PARTIDO { get; set; }
        public long SQ_COLIGACAO { get; set; }
        public string NM_COLIGACAO { get; set; }
        public string DS_COMPOSICAO_COLIGACAO { get; set; }
        public int CD_NACIONALIDADE { get; set; }
        public string DS_NACIONALIDADE { get; set; }
        public string SG_UF_NASCIMENTO { get; set; }
        public int CD_MUNICIPIO_NASCIMENTO { get; set; }
        public string NM_MUNICIPIO_NASCIMENTO { get; set; }
        public DateTime DT_NASCIMENTO { get; set; }
        public int NR_IDADE_DATA_POSSE { get; set; }
        public long NR_TITULO_ELEITORAL_CANDIDATO { get; set; }
        public int CD_GENERO { get; set; }
        public string DS_GENERO { get; set; }
        public int CD_GRAU_INSTRUCAO { get; set; }
        public string DS_GRAU_INSTRUCAO { get; set; }
        public int CD_ESTADO_CIVIL { get; set; }
        public string DS_ESTADO_CIVIL { get; set; }
        public int CD_COR_RACA { get; set; }
        public string DS_COR_RACA { get; set; }
        public int CD_OCUPACAO { get; set; }
        public string DS_OCUPACAO { get; set; }
        public int NR_DESPESA_MAX_CAMPANHA { get; set; }
        public int CD_SIT_TOT_TURNO { get; set; }
        public string DS_SIT_TOT_TURNO { get; set; }
        public string ST_REELEICAO { get; set; }
        public string ST_DECLARAR_BENS { get; set; }
        public int NR_PROTOCOLO_CANDIDATURA { get; set; }
        public long NR_PROCESSO { get; set; }


        public bool Equal(Politico outer)
        {
            return outer != null
                && DT_GERACAO == outer.DT_GERACAO
                && HH_GERACAO == outer.HH_GERACAO
                && ANO_ELEICAO == outer.ANO_ELEICAO
                && CD_TIPO_ELEICAO == outer.CD_TIPO_ELEICAO
                && NM_TIPO_ELEICAO == outer.NM_TIPO_ELEICAO
                && NR_TURNO == outer.NR_TURNO
                && CD_ELEICAO == outer.CD_ELEICAO
                && DS_ELEICAO == outer.DS_ELEICAO
                && DT_ELEICAO == outer.DT_ELEICAO
                && TP_ABRANGENCIA == outer.TP_ABRANGENCIA
                && SG_UF == outer.SG_UF
                && SG_UE == outer.SG_UE
                && NM_UE == outer.NM_UE
                && CD_CARGO == outer.CD_CARGO
                && DS_CARGO == outer.DS_CARGO
                && SQ_CANDIDATO == outer.SQ_CANDIDATO
                && NR_CANDIDATO == outer.NR_CANDIDATO
                && NM_CANDIDATO == outer.NM_CANDIDATO
                && NM_URNA_CANDIDATO == outer.NM_URNA_CANDIDATO
                && NM_SOCIAL_CANDIDATO == outer.NM_SOCIAL_CANDIDATO
                && NR_CPF_CANDIDATO == outer.NR_CPF_CANDIDATO
                && NM_EMAIL == outer.NM_EMAIL
                && CD_SITUACAO_CANDIDATURA == outer.CD_SITUACAO_CANDIDATURA
                && DS_SITUACAO_CANDIDATURA == outer.DS_SITUACAO_CANDIDATURA
                && CD_DETALHE_SITUACAO_CAND == outer.CD_DETALHE_SITUACAO_CAND
                && DS_DETALHE_SITUACAO_CAND == outer.DS_DETALHE_SITUACAO_CAND
                && TP_AGREMIACAO == outer.TP_AGREMIACAO
                && NR_PARTIDO == outer.NR_PARTIDO
                && SG_PARTIDO == outer.SG_PARTIDO
                && NM_PARTIDO == outer.NM_PARTIDO
                && SQ_COLIGACAO == outer.SQ_COLIGACAO
                && NM_COLIGACAO == outer.NM_COLIGACAO
                && DS_COMPOSICAO_COLIGACAO == outer.DS_COMPOSICAO_COLIGACAO
                && CD_NACIONALIDADE == outer.CD_NACIONALIDADE
                && DS_NACIONALIDADE == outer.DS_NACIONALIDADE
                && SG_UF_NASCIMENTO == outer.SG_UF_NASCIMENTO
                && CD_MUNICIPIO_NASCIMENTO == outer.CD_MUNICIPIO_NASCIMENTO
                && NM_MUNICIPIO_NASCIMENTO == outer.NM_MUNICIPIO_NASCIMENTO
                && DT_NASCIMENTO == outer.DT_NASCIMENTO
                && NR_IDADE_DATA_POSSE == outer.NR_IDADE_DATA_POSSE
                && NR_TITULO_ELEITORAL_CANDIDATO == outer.NR_TITULO_ELEITORAL_CANDIDATO
                && CD_GENERO == outer.CD_GENERO
                && DS_GENERO == outer.DS_GENERO
                && CD_GRAU_INSTRUCAO == outer.CD_GRAU_INSTRUCAO
                && DS_GRAU_INSTRUCAO == outer.DS_GRAU_INSTRUCAO
                && CD_ESTADO_CIVIL == outer.CD_ESTADO_CIVIL
                && DS_ESTADO_CIVIL == outer.DS_ESTADO_CIVIL
                && CD_COR_RACA == outer.CD_COR_RACA
                && DS_COR_RACA == outer.DS_COR_RACA
                && CD_OCUPACAO == outer.CD_OCUPACAO
                && DS_OCUPACAO == outer.DS_OCUPACAO
                && NR_DESPESA_MAX_CAMPANHA == outer.NR_DESPESA_MAX_CAMPANHA
                && CD_SIT_TOT_TURNO == outer.CD_SIT_TOT_TURNO
                && DS_SIT_TOT_TURNO == outer.DS_SIT_TOT_TURNO
                && ST_REELEICAO == outer.ST_REELEICAO
                && ST_DECLARAR_BENS == outer.ST_DECLARAR_BENS
                && NR_PROTOCOLO_CANDIDATURA == outer.NR_PROTOCOLO_CANDIDATURA
                && NR_PROCESSO == outer.NR_PROCESSO;
        }
    }
}