using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Despesas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeputadoId = table.Column<long>(nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    CnpjCpfFornecedor = table.Column<string>(nullable: true),
                    CodDocumento = table.Column<long>(nullable: false),
                    CodLote = table.Column<long>(nullable: false),
                    CodTipoDocumento = table.Column<int>(nullable: true),
                    DataDocumento = table.Column<DateTime>(nullable: true),
                    Mes = table.Column<int>(nullable: false),
                    NomeFornecedor = table.Column<string>(nullable: true),
                    NumDocumento = table.Column<string>(nullable: true),
                    NumRessarcimento = table.Column<string>(nullable: true),
                    Parcela = table.Column<int>(nullable: false),
                    TipoDespesa = table.Column<string>(nullable: true),
                    TipoDocumento = table.Column<string>(nullable: true),
                    URLDocumento = table.Column<string>(nullable: true),
                    ValorDocumento = table.Column<decimal>(nullable: false),
                    ValorGlosa = table.Column<decimal>(nullable: false),
                    ValorLiquido = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Legislaturas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legislaturas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partidos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Sigla = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: true),
                    LegislaturaId = table.Column<long>(nullable: false),
                    Situacao = table.Column<string>(nullable: true),
                    TotalPosse = table.Column<int>(nullable: false),
                    TotalMembros = table.Column<int>(nullable: false),
                    LiderId = table.Column<string>(nullable: true),
                    UrlFacebook = table.Column<string>(nullable: true),
                    UrlLogo = table.Column<string>(nullable: true),
                    UrlWebSite = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Politicos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CPF = table.Column<string>(nullable: true),
                    DataFalecimento = table.Column<DateTime>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    Escolaridade = table.Column<string>(nullable: true),
                    MunicipioNascimento = table.Column<string>(nullable: true),
                    NomeCivil = table.Column<string>(nullable: true),
                    RedeSocial = table.Column<string>(nullable: true),
                    Sexo = table.Column<string>(nullable: true),
                    UFNascimento = table.Column<string>(nullable: true),
                    URLWebsite = table.Column<string>(nullable: true),
                    LegislaturaId = table.Column<long>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    NomeEleitoral = table.Column<string>(nullable: true),
                    SiglaPartido = table.Column<string>(nullable: true),
                    SiglaUf = table.Column<string>(nullable: true),
                    Situacao = table.Column<string>(nullable: true),
                    PartidoId = table.Column<long>(nullable: false),
                    URLFoto = table.Column<string>(nullable: true),
                    CondicaoEleitoral = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: true),
                    DescricaoStatus = table.Column<string>(nullable: true),
                    GabineteAndar = table.Column<string>(nullable: true),
                    GabineteEmail = table.Column<string>(nullable: true),
                    GabineteNome = table.Column<string>(nullable: true),
                    GabinetePredio = table.Column<string>(nullable: true),
                    GabineteSala = table.Column<string>(nullable: true),
                    GabineteTelefone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Politicos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Despesas");

            migrationBuilder.DropTable(
                name: "Legislaturas");

            migrationBuilder.DropTable(
                name: "Partidos");

            migrationBuilder.DropTable(
                name: "Politicos");
        }
    }
}
