﻿// <auto-generated />
using System;
using DeOlho.ETL.tse_jus_br.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeOlho.ETL.tse_jus_br.Infrastructure.Migrations
{
    [DbContext(typeof(DeOlhoDbContext))]
    partial class DeOlhoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DeOlho.ETL.tse_jus_br.Domain.Importacao", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DataHoraArquivo");

                    b.Property<DateTime>("DataHoraImportacao");

                    b.Property<string>("FileName");

                    b.Property<string>("UrlOrigem");

                    b.HasKey("Id");

                    b.ToTable("Importacao");
                });

            modelBuilder.Entity("DeOlho.ETL.tse_jus_br.Domain.Politico", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ANO_ELEICAO");

                    b.Property<int>("CD_CARGO");

                    b.Property<int>("CD_COR_RACA");

                    b.Property<int>("CD_DETALHE_SITUACAO_CAND");

                    b.Property<int>("CD_ELEICAO");

                    b.Property<int>("CD_ESTADO_CIVIL");

                    b.Property<int>("CD_GENERO");

                    b.Property<int>("CD_GRAU_INSTRUCAO");

                    b.Property<int>("CD_MUNICIPIO_NASCIMENTO");

                    b.Property<int>("CD_NACIONALIDADE");

                    b.Property<int>("CD_OCUPACAO");

                    b.Property<int>("CD_SITUACAO_CANDIDATURA");

                    b.Property<int>("CD_SIT_TOT_TURNO");

                    b.Property<int>("CD_TIPO_ELEICAO");

                    b.Property<string>("DS_CARGO");

                    b.Property<string>("DS_COMPOSICAO_COLIGACAO");

                    b.Property<string>("DS_COR_RACA");

                    b.Property<string>("DS_DETALHE_SITUACAO_CAND");

                    b.Property<string>("DS_ELEICAO");

                    b.Property<string>("DS_ESTADO_CIVIL");

                    b.Property<string>("DS_GENERO");

                    b.Property<string>("DS_GRAU_INSTRUCAO");

                    b.Property<string>("DS_NACIONALIDADE");

                    b.Property<string>("DS_OCUPACAO");

                    b.Property<string>("DS_SITUACAO_CANDIDATURA");

                    b.Property<string>("DS_SIT_TOT_TURNO");

                    b.Property<DateTime>("DT_ELEICAO");

                    b.Property<DateTime>("DT_NASCIMENTO");

                    b.Property<long>("ImportacaoId");

                    b.Property<string>("NM_CANDIDATO");

                    b.Property<string>("NM_COLIGACAO");

                    b.Property<string>("NM_EMAIL");

                    b.Property<string>("NM_MUNICIPIO_NASCIMENTO");

                    b.Property<string>("NM_PARTIDO");

                    b.Property<string>("NM_SOCIAL_CANDIDATO");

                    b.Property<string>("NM_TIPO_ELEICAO");

                    b.Property<string>("NM_UE");

                    b.Property<string>("NM_URNA_CANDIDATO");

                    b.Property<int>("NR_CANDIDATO");

                    b.Property<long>("NR_CPF_CANDIDATO");

                    b.Property<int>("NR_DESPESA_MAX_CAMPANHA");

                    b.Property<int>("NR_IDADE_DATA_POSSE");

                    b.Property<int>("NR_PARTIDO");

                    b.Property<long>("NR_PROCESSO");

                    b.Property<int>("NR_PROTOCOLO_CANDIDATURA");

                    b.Property<long>("NR_TITULO_ELEITORAL_CANDIDATO");

                    b.Property<int>("NR_TURNO");

                    b.Property<string>("SG_PARTIDO");

                    b.Property<string>("SG_UE");

                    b.Property<string>("SG_UF");

                    b.Property<string>("SG_UF_NASCIMENTO");

                    b.Property<long>("SQ_CANDIDATO");

                    b.Property<long>("SQ_COLIGACAO");

                    b.Property<string>("ST_DECLARAR_BENS");

                    b.Property<string>("ST_REELEICAO");

                    b.Property<string>("TP_ABRANGENCIA");

                    b.Property<string>("TP_AGREMIACAO");

                    b.HasKey("Id");

                    b.HasIndex("ImportacaoId");

                    b.HasIndex("NR_CPF_CANDIDATO")
                        .IsUnique();

                    b.ToTable("Politico");
                });

            modelBuilder.Entity("DeOlho.ETL.tse_jus_br.Domain.Politico", b =>
                {
                    b.HasOne("DeOlho.ETL.tse_jus_br.Domain.Importacao", "Importacao")
                        .WithMany("Politicos")
                        .HasForeignKey("ImportacaoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
