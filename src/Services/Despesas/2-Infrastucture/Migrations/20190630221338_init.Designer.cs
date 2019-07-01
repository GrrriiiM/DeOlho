﻿// <auto-generated />
using DeOlho.Services.Despesas.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeOlho.Services.Despesas.Infrastucture.Migrations
{
    [DbContext(typeof(DeOlhoDbContext))]
    [Migration("20190630221338_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DeOlho.Services.Despesas.Domain.PoliticoAno", b =>
                {
                    b.Property<long>("CPF");

                    b.Property<int>("Ano");

                    b.Property<decimal>("Valor");

                    b.HasKey("CPF", "Ano");

                    b.ToTable("PoliticoAno");
                });

            modelBuilder.Entity("DeOlho.Services.Despesas.Domain.PoliticoAnoMesTipo", b =>
                {
                    b.Property<long>("CPF");

                    b.Property<int>("Mes");

                    b.Property<int>("Ano");

                    b.Property<int>("TipoId");

                    b.Property<decimal>("Valor");

                    b.HasKey("CPF", "Mes", "Ano", "TipoId");

                    b.ToTable("PoliticoAnoMesTipo");
                });

            modelBuilder.Entity("DeOlho.Services.Despesas.Domain.PoliticoAnoTipo", b =>
                {
                    b.Property<long>("CPF");

                    b.Property<int>("Ano");

                    b.Property<int>("TipoId");

                    b.Property<decimal>("Valor");

                    b.HasKey("CPF", "Ano", "TipoId");

                    b.ToTable("PoliticoAnoTipo");
                });

            modelBuilder.Entity("DeOlho.Services.Despesas.Domain.PoliticoMes", b =>
                {
                    b.Property<long>("CPF");

                    b.Property<int>("Mes");

                    b.Property<int>("Ano");

                    b.Property<decimal>("Valor");

                    b.HasKey("CPF", "Mes", "Ano");

                    b.ToTable("PoliticoMes");
                });

            modelBuilder.Entity("DeOlho.Services.Despesas.Domain.TotalAno", b =>
                {
                    b.Property<int>("Ano")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Valor");

                    b.HasKey("Ano");

                    b.ToTable("TotalAno");
                });

            modelBuilder.Entity("DeOlho.Services.Despesas.Domain.TotalAnoMes", b =>
                {
                    b.Property<int>("Mes");

                    b.Property<int>("Ano");

                    b.Property<decimal>("Valor");

                    b.HasKey("Mes", "Ano");

                    b.ToTable("TotalAnoMes");
                });

            modelBuilder.Entity("DeOlho.Services.Despesas.Domain.TotalPolitico", b =>
                {
                    b.Property<long>("CPF")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Valor");

                    b.HasKey("CPF");

                    b.ToTable("TotalPolitico");
                });

            modelBuilder.Entity("DeOlho.Services.Despesas.Domain.TotalTipo", b =>
                {
                    b.Property<int>("TipoId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Valor");

                    b.HasKey("TipoId");

                    b.ToTable("TotalTipo");
                });
#pragma warning restore 612, 618
        }
    }
}
