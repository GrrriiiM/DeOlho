using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeOlho.ETL.dadosabertos_leg_br.Infrastructure.Migrations
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
                    OrigemId = table.Column<long>(nullable: false),
                    CPF = table.Column<long>(nullable: false),
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
                name: "NotaFiscal",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeputadoFederalId = table.Column<long>(nullable: false),
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
                    table.PrimaryKey("PK_NotaFiscal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotaFiscal_DeputadoFederal_DeputadoFederalId",
                        column: x => x.DeputadoFederalId,
                        principalTable: "DeputadoFederal",
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
                name: "IX_NotaFiscal_CodDocumento",
                table: "NotaFiscal",
                column: "CodDocumento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_DeputadoFederalId",
                table: "NotaFiscal",
                column: "DeputadoFederalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotaFiscal");

            migrationBuilder.DropTable(
                name: "DeputadoFederal");
        }
    }
}
