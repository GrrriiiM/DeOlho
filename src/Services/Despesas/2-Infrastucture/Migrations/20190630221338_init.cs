using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeOlho.Services.Despesas.Infrastucture.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PoliticoAno",
                columns: table => new
                {
                    CPF = table.Column<long>(nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticoAno", x => new { x.CPF, x.Ano });
                });

            migrationBuilder.CreateTable(
                name: "PoliticoAnoMesTipo",
                columns: table => new
                {
                    CPF = table.Column<long>(nullable: false),
                    TipoId = table.Column<int>(nullable: false),
                    Mes = table.Column<int>(nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticoAnoMesTipo", x => new { x.CPF, x.Mes, x.Ano, x.TipoId });
                });

            migrationBuilder.CreateTable(
                name: "PoliticoAnoTipo",
                columns: table => new
                {
                    CPF = table.Column<long>(nullable: false),
                    TipoId = table.Column<int>(nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticoAnoTipo", x => new { x.CPF, x.Ano, x.TipoId });
                });

            migrationBuilder.CreateTable(
                name: "PoliticoMes",
                columns: table => new
                {
                    CPF = table.Column<long>(nullable: false),
                    Mes = table.Column<int>(nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticoMes", x => new { x.CPF, x.Mes, x.Ano });
                });

            migrationBuilder.CreateTable(
                name: "TotalAno",
                columns: table => new
                {
                    Ano = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalAno", x => x.Ano);
                });

            migrationBuilder.CreateTable(
                name: "TotalAnoMes",
                columns: table => new
                {
                    Mes = table.Column<int>(nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalAnoMes", x => new { x.Mes, x.Ano });
                });

            migrationBuilder.CreateTable(
                name: "TotalPolitico",
                columns: table => new
                {
                    CPF = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalPolitico", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "TotalTipo",
                columns: table => new
                {
                    TipoId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalTipo", x => x.TipoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoliticoAno");

            migrationBuilder.DropTable(
                name: "PoliticoAnoMesTipo");

            migrationBuilder.DropTable(
                name: "PoliticoAnoTipo");

            migrationBuilder.DropTable(
                name: "PoliticoMes");

            migrationBuilder.DropTable(
                name: "TotalAno");

            migrationBuilder.DropTable(
                name: "TotalAnoMes");

            migrationBuilder.DropTable(
                name: "TotalPolitico");

            migrationBuilder.DropTable(
                name: "TotalTipo");
        }
    }
}
