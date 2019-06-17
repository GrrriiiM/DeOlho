using System;
using DeOlho.ETL.tse_jus_br.Domain.Events;
using DeOlho.ETL.tse_jus_br.Domain.SeedWork;

namespace DeOlho.ETL.tse_jus_br.Domain
{
    public class Politico : Entity
    {
        internal Politico() {}
        
        // internal Politico(Importacao importacao, dynamic registroImportacao)
        // {
        //     this.Importacao = importacao;
        //     this.ImportacaoId = importacao.Id;

        //     this.setValues(registroImportacao);

        //     AddDomainEvent(new PoliticoCreatedDomainEvent(this));
        // }

        public Importacao Importacao {get; protected set;}
        public long ImportacaoId {get; protected set; }

        public int ANO_ELEICAO { get; protected set; }
        public int CD_TIPO_ELEICAO { get; protected set; }
        public string NM_TIPO_ELEICAO { get; protected set; }
        public int NR_TURNO { get; protected set; }
        public int CD_ELEICAO { get; protected set; }
        public string DS_ELEICAO { get; protected set; }
        public DateTime DT_ELEICAO { get; protected set; }
        public string TP_ABRANGENCIA { get; protected set; }
        public string SG_UF { get; protected set; }
        public string SG_UE { get; protected set; }
        public string NM_UE { get; protected set; }
        public int CD_CARGO { get; protected set; }
        public string DS_CARGO { get; protected set; }
        public long SQ_CANDIDATO { get; protected set; }
        public int NR_CANDIDATO { get; protected set; }
        public string NM_CANDIDATO { get; protected set; }
        public string NM_URNA_CANDIDATO { get; protected set; }
        public string NM_SOCIAL_CANDIDATO { get; protected set; }
        public long NR_CPF_CANDIDATO { get; protected set; }
        public string NM_EMAIL { get; protected set; }
        public int CD_SITUACAO_CANDIDATURA { get; protected set; }
        public string DS_SITUACAO_CANDIDATURA { get; protected set; }
        public int CD_DETALHE_SITUACAO_CAND { get; protected set; }
        public string DS_DETALHE_SITUACAO_CAND { get; protected set; }
        public string TP_AGREMIACAO { get; protected set; }
        public int NR_PARTIDO { get; protected set; }
        public string SG_PARTIDO { get; protected set; }
        public string NM_PARTIDO { get; protected set; }
        public long SQ_COLIGACAO { get; protected set; }
        public string NM_COLIGACAO { get; protected set; }
        public string DS_COMPOSICAO_COLIGACAO { get; protected set; }
        public int CD_NACIONALIDADE { get; protected set; }
        public string DS_NACIONALIDADE { get; protected set; }
        public string SG_UF_NASCIMENTO { get; protected set; }
        public int CD_MUNICIPIO_NASCIMENTO { get; protected set; }
        public string NM_MUNICIPIO_NASCIMENTO { get; protected set; }
        public DateTime DT_NASCIMENTO { get; protected set; }
        public int NR_IDADE_DATA_POSSE { get; protected set; }
        public long NR_TITULO_ELEITORAL_CANDIDATO { get; protected set; }
        public int CD_GENERO { get; protected set; }
        public string DS_GENERO { get; protected set; }
        public int CD_GRAU_INSTRUCAO { get; protected set; }
        public string DS_GRAU_INSTRUCAO { get; protected set; }
        public int CD_ESTADO_CIVIL { get; protected set; }
        public string DS_ESTADO_CIVIL { get; protected set; }
        public int CD_COR_RACA { get; protected set; }
        public string DS_COR_RACA { get; protected set; }
        public int CD_OCUPACAO { get; protected set; }
        public string DS_OCUPACAO { get; protected set; }
        public int NR_DESPESA_MAX_CAMPANHA { get; protected set; }
        public int CD_SIT_TOT_TURNO { get; protected set; }
        public string DS_SIT_TOT_TURNO { get; protected set; }
        public string ST_REELEICAO { get; protected set; }
        public string ST_DECLARAR_BENS { get; protected set; }
        public int NR_PROTOCOLO_CANDIDATURA { get; protected set; }
        public long NR_PROCESSO { get; protected set; }

        internal void SetImportacao(Importacao importacao)
        {
            Importacao = importacao;
            ImportacaoId = importacao.Id;
        }

        // public bool HasChange(Politico outer)
        // {
        //     return outer != null
        //         && ANO_ELEICAO == outer.ANO_ELEICAO
        //         && CD_TIPO_ELEICAO == outer.CD_TIPO_ELEICAO
        //         && NM_TIPO_ELEICAO == outer.NM_TIPO_ELEICAO
        //         && NR_TURNO == outer.NR_TURNO
        //         && CD_ELEICAO == outer.CD_ELEICAO
        //         && DS_ELEICAO == outer.DS_ELEICAO
        //         && DT_ELEICAO == outer.DT_ELEICAO
        //         && TP_ABRANGENCIA == outer.TP_ABRANGENCIA
        //         && SG_UF == outer.SG_UF
        //         && SG_UE == outer.SG_UE
        //         && NM_UE == outer.NM_UE
        //         && CD_CARGO == outer.CD_CARGO
        //         && DS_CARGO == outer.DS_CARGO
        //         && SQ_CANDIDATO == outer.SQ_CANDIDATO
        //         && NR_CANDIDATO == outer.NR_CANDIDATO
        //         && NM_CANDIDATO == outer.NM_CANDIDATO
        //         && NM_URNA_CANDIDATO == outer.NM_URNA_CANDIDATO
        //         && NM_SOCIAL_CANDIDATO == outer.NM_SOCIAL_CANDIDATO
        //         && NR_CPF_CANDIDATO == outer.NR_CPF_CANDIDATO
        //         && NM_EMAIL == outer.NM_EMAIL
        //         && CD_SITUACAO_CANDIDATURA == outer.CD_SITUACAO_CANDIDATURA
        //         && DS_SITUACAO_CANDIDATURA == outer.DS_SITUACAO_CANDIDATURA
        //         && CD_DETALHE_SITUACAO_CAND == outer.CD_DETALHE_SITUACAO_CAND
        //         && DS_DETALHE_SITUACAO_CAND == outer.DS_DETALHE_SITUACAO_CAND
        //         && TP_AGREMIACAO == outer.TP_AGREMIACAO
        //         && NR_PARTIDO == outer.NR_PARTIDO
        //         && SG_PARTIDO == outer.SG_PARTIDO
        //         && NM_PARTIDO == outer.NM_PARTIDO
        //         && SQ_COLIGACAO == outer.SQ_COLIGACAO
        //         && NM_COLIGACAO == outer.NM_COLIGACAO
        //         && DS_COMPOSICAO_COLIGACAO == outer.DS_COMPOSICAO_COLIGACAO
        //         && CD_NACIONALIDADE == outer.CD_NACIONALIDADE
        //         && DS_NACIONALIDADE == outer.DS_NACIONALIDADE
        //         && SG_UF_NASCIMENTO == outer.SG_UF_NASCIMENTO
        //         && CD_MUNICIPIO_NASCIMENTO == outer.CD_MUNICIPIO_NASCIMENTO
        //         && NM_MUNICIPIO_NASCIMENTO == outer.NM_MUNICIPIO_NASCIMENTO
        //         && DT_NASCIMENTO == outer.DT_NASCIMENTO
        //         && NR_IDADE_DATA_POSSE == outer.NR_IDADE_DATA_POSSE
        //         && NR_TITULO_ELEITORAL_CANDIDATO == outer.NR_TITULO_ELEITORAL_CANDIDATO
        //         && CD_GENERO == outer.CD_GENERO
        //         && DS_GENERO == outer.DS_GENERO
        //         && CD_GRAU_INSTRUCAO == outer.CD_GRAU_INSTRUCAO
        //         && DS_GRAU_INSTRUCAO == outer.DS_GRAU_INSTRUCAO
        //         && CD_ESTADO_CIVIL == outer.CD_ESTADO_CIVIL
        //         && DS_ESTADO_CIVIL == outer.DS_ESTADO_CIVIL
        //         && CD_COR_RACA == outer.CD_COR_RACA
        //         && DS_COR_RACA == outer.DS_COR_RACA
        //         && CD_OCUPACAO == outer.CD_OCUPACAO
        //         && DS_OCUPACAO == outer.DS_OCUPACAO
        //         && NR_DESPESA_MAX_CAMPANHA == outer.NR_DESPESA_MAX_CAMPANHA
        //         && CD_SIT_TOT_TURNO == outer.CD_SIT_TOT_TURNO
        //         && DS_SIT_TOT_TURNO == outer.DS_SIT_TOT_TURNO
        //         && ST_REELEICAO == outer.ST_REELEICAO
        //         && ST_DECLARAR_BENS == outer.ST_DECLARAR_BENS
        //         && NR_PROTOCOLO_CANDIDATURA == outer.NR_PROTOCOLO_CANDIDATURA
        //         && NR_PROCESSO == outer.NR_PROCESSO;
        // }

        // private void setValues(dynamic registroImportacao) 
        // {
        //     ANO_ELEICAO = int.Parse(registroImportacao.ANO_ELEICAO);
        //     CD_TIPO_ELEICAO = int.Parse(registroImportacao.CD_TIPO_ELEICAO);
        //     NM_TIPO_ELEICAO = registroImportacao.NM_TIPO_ELEICAO;
        //     NR_TURNO = int.Parse(registroImportacao.NR_TURNO);
        //     CD_ELEICAO = int.Parse(registroImportacao.CD_ELEICAO);
        //     DS_ELEICAO = registroImportacao.DS_ELEICAO;
        //     DT_ELEICAO = DateTime.Parse(registroImportacao.DT_ELEICAO);
        //     TP_ABRANGENCIA = registroImportacao.TP_ABRANGENCIA;
        //     SG_UF = registroImportacao.SG_UF;
        //     SG_UE = registroImportacao.SG_UE;
        //     NM_UE = registroImportacao.NM_UE;
        //     CD_CARGO = int.Parse(registroImportacao.CD_CARGO);
        //     DS_CARGO = registroImportacao.DS_CARGO;
        //     SQ_CANDIDATO = long.Parse(registroImportacao.SQ_CANDIDATO);
        //     NR_CANDIDATO = int.Parse(registroImportacao.NR_CANDIDATO);
        //     NM_CANDIDATO = registroImportacao.NM_CANDIDATO;
        //     NM_URNA_CANDIDATO = registroImportacao.NM_URNA_CANDIDATO;
        //     NM_SOCIAL_CANDIDATO = registroImportacao.NM_SOCIAL_CANDIDATO;
        //     NR_CPF_CANDIDATO = long.Parse(registroImportacao.NR_CPF_CANDIDATO);
        //     NM_EMAIL = registroImportacao.NM_EMAIL;
        //     CD_SITUACAO_CANDIDATURA = int.Parse(registroImportacao.CD_SITUACAO_CANDIDATURA);
        //     DS_SITUACAO_CANDIDATURA = registroImportacao.DS_SITUACAO_CANDIDATURA;
        //     CD_DETALHE_SITUACAO_CAND = int.Parse(registroImportacao.CD_DETALHE_SITUACAO_CAND);
        //     DS_DETALHE_SITUACAO_CAND = registroImportacao.DS_DETALHE_SITUACAO_CAND;
        //     TP_AGREMIACAO = registroImportacao.TP_AGREMIACAO;
        //     NR_PARTIDO = int.Parse(registroImportacao.NR_PARTIDO);
        //     SG_PARTIDO = registroImportacao.SG_PARTIDO;
        //     NM_PARTIDO = registroImportacao.NM_PARTIDO;
        //     SQ_COLIGACAO = long.Parse(registroImportacao.SQ_COLIGACAO);
        //     NM_COLIGACAO = registroImportacao.NM_COLIGACAO;
        //     DS_COMPOSICAO_COLIGACAO = registroImportacao.DS_COMPOSICAO_COLIGACAO;
        //     CD_NACIONALIDADE = int.Parse(registroImportacao.CD_NACIONALIDADE);
        //     DS_NACIONALIDADE = registroImportacao.DS_NACIONALIDADE;
        //     SG_UF_NASCIMENTO = registroImportacao.SG_UF_NASCIMENTO;
        //     CD_MUNICIPIO_NASCIMENTO = int.Parse(registroImportacao.CD_MUNICIPIO_NASCIMENTO);
        //     NM_MUNICIPIO_NASCIMENTO = registroImportacao.NM_MUNICIPIO_NASCIMENTO;
        //     DT_NASCIMENTO = DateTime.Parse(registroImportacao.DT_NASCIMENTO);
        //     NR_IDADE_DATA_POSSE = int.Parse(registroImportacao.NR_IDADE_DATA_POSSE);
        //     NR_TITULO_ELEITORAL_CANDIDATO = long.Parse(registroImportacao.NR_TITULO_ELEITORAL_CANDIDATO);
        //     CD_GENERO = int.Parse(registroImportacao.CD_GENERO);
        //     DS_GENERO = registroImportacao.DS_GENERO;
        //     CD_GRAU_INSTRUCAO = int.Parse(registroImportacao.CD_GRAU_INSTRUCAO);
        //     DS_GRAU_INSTRUCAO = registroImportacao.DS_GRAU_INSTRUCAO;
        //     CD_ESTADO_CIVIL = int.Parse(registroImportacao.CD_ESTADO_CIVIL);
        //     DS_ESTADO_CIVIL = registroImportacao.DS_ESTADO_CIVIL;
        //     CD_COR_RACA = int.Parse(registroImportacao.CD_COR_RACA);
        //     DS_COR_RACA = registroImportacao.DS_COR_RACA;
        //     CD_OCUPACAO = int.Parse(registroImportacao.CD_OCUPACAO);
        //     DS_OCUPACAO = registroImportacao.DS_OCUPACAO;
        //     NR_DESPESA_MAX_CAMPANHA = int.Parse(registroImportacao.NR_DESPESA_MAX_CAMPANHA);
        //     CD_SIT_TOT_TURNO = int.Parse(registroImportacao.CD_SIT_TOT_TURNO);
        //     DS_SIT_TOT_TURNO = registroImportacao.DS_SIT_TOT_TURNO;
        //     ST_REELEICAO = registroImportacao.ST_REELEICAO;
        //     ST_DECLARAR_BENS = registroImportacao.ST_DECLARAR_BENS;
        //     NR_PROTOCOLO_CANDIDATURA = int.Parse(registroImportacao.NR_PROTOCOLO_CANDIDATURA);
        //     NR_PROCESSO = long.Parse(registroImportacao.NR_PROCESSO);
        // }

    }
}