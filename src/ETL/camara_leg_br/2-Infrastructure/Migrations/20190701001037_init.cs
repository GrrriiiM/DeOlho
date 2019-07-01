using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeOlho.ETL.camara_leg_br.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeputadoFederal",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CPF = table.Column<long>(nullable: false),
                    OrigemId = table.Column<long>(nullable: false),
                    URLWebsite = table.Column<string>(nullable: true),
                    URLFoto = table.Column<string>(nullable: true),
                    CondicaoEleitoral = table.Column<string>(nullable: true),
                    GabineteNome = table.Column<string>(nullable: true),
                    GabinetePredio = table.Column<string>(nullable: true),
                    GabineteSala = table.Column<string>(nullable: true),
                    GabineteAndar = table.Column<string>(nullable: true),
                    GabineteTelefone = table.Column<string>(nullable: true),
                    GabineteEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeputadoFederal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotaFiscalPeriodo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeputadoFederalId = table.Column<long>(nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    Mes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaFiscalPeriodo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotaFiscalPeriodo_DeputadoFederal_DeputadoFederalId",
                        column: x => x.DeputadoFederalId,
                        principalTable: "DeputadoFederal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotaFiscal",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PeriodoId = table.Column<long>(nullable: false),
                    CnpjCpfFornecedor = table.Column<string>(nullable: true),
                    CodDocumento = table.Column<long>(nullable: false),
                    CodLote = table.Column<long>(nullable: false),
                    CodTipoDocumento = table.Column<int>(nullable: false),
                    DataDocumento = table.Column<DateTime>(nullable: false),
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
                    table.PrimaryKey("PK_NotaFiscal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotaFiscal_NotaFiscalPeriodo_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "NotaFiscalPeriodo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeputadoFederal_CPF",
                table: "DeputadoFederal",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeputadoFederal_OrigemId",
                table: "DeputadoFederal",
                column: "OrigemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_PeriodoId_CodDocumento_CnpjCpfFornecedor_CodTipoD~",
                table: "NotaFiscal",
                columns: new[] { "PeriodoId", "CodDocumento", "CnpjCpfFornecedor", "CodTipoDocumento", "DataDocumento", "NumDocumento" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscalPeriodo_DeputadoFederalId_Ano_Mes",
                table: "NotaFiscalPeriodo",
                columns: new[] { "DeputadoFederalId", "Ano", "Mes" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotaFiscal");

            migrationBuilder.DropTable(
                name: "NotaFiscalPeriodo");

            migrationBuilder.DropTable(
                name: "DeputadoFederal");
        }
    }
}
