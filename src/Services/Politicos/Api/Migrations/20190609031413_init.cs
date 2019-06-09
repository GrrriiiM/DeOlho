using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeOlho.Services.Politicos.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContatoTipo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContatoTipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MandatoSituacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandatoSituacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MandatoTipo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandatoTipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoliticoEscolaridade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticoEscolaridade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Politicos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IntegrationTimestamp = table.Column<long>(nullable: false),
                    IntegrationOrigin = table.Column<string>(nullable: true),
                    IntegrationId = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Apelido = table.Column<string>(nullable: true),
                    Nascimento = table.Column<DateTime>(nullable: false),
                    Falecimento = table.Column<DateTime>(nullable: true),
                    NascimentoUF = table.Column<string>(nullable: true),
                    NascimentoMunicipio = table.Column<string>(nullable: true),
                    EscolaridadeId = table.Column<int>(nullable: false),
                    SituacaoId = table.Column<int>(nullable: false),
                    SexoId = table.Column<int>(nullable: false),
                    MandatoTipoId = table.Column<int>(nullable: false),
                    MandatoInicio = table.Column<DateTime>(nullable: true),
                    MandatoFim = table.Column<DateTime>(nullable: true),
                    MandatoSituacaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Politicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoliticoSexo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticoSexo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoliticoSituacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticoSituacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoliticoContato",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IntegrationTimestamp = table.Column<long>(nullable: false),
                    IntegrationOrigin = table.Column<string>(nullable: true),
                    IntegrationId = table.Column<string>(nullable: true),
                    PoliticoId = table.Column<long>(nullable: false),
                    TipoId = table.Column<int>(nullable: false),
                    Contato = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticoContato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliticoContato_Politicos_PoliticoId",
                        column: x => x.PoliticoId,
                        principalTable: "Politicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoliticoContato_ContatoTipo_TipoId",
                        column: x => x.TipoId,
                        principalTable: "ContatoTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MandatoSituacao",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Nenhum" },
                    { 2, "Exercicio" },
                    { 3, "Afastado" },
                    { 4, "Convocado" },
                    { 5, "FimMandato" },
                    { 6, "Licenca" },
                    { 7, "Suplencia" },
                    { 8, "Suspenso" },
                    { 9, "Vacancia" }
                });

            migrationBuilder.InsertData(
                table: "MandatoTipo",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "DeputadoFederal" },
                    { 1, "Email" }
                });

            migrationBuilder.InsertData(
                table: "PoliticoEscolaridade",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 99, "INCORRETO" },
                    { 16, "Doutorado" },
                    { 15, "DoutoradoIncompleto" },
                    { 14, "Mestrado" },
                    { 13, "MestradoIncompleto" },
                    { 12, "PosGraduacao" },
                    { 11, "PosGraduacaoIncompleto" },
                    { 10, "Superior" },
                    { 8, "Tecnico" },
                    { 7, "TecnicoIncompleto" },
                    { 6, "Medio" },
                    { 5, "MedioIncompleto" },
                    { 4, "Fundamental" },
                    { 3, "FundamentalIncompleto" },
                    { 2, "LeEscreve" },
                    { 1, "Analfabeto" },
                    { 9, "SuperiorIncomplete" }
                });

            migrationBuilder.InsertData(
                table: "PoliticoSexo",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Masculino" },
                    { 2, "Feminino" },
                    { 3, "Outro" },
                    { 99, "INCORRETO" }
                });

            migrationBuilder.InsertData(
                table: "PoliticoSituacao",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Preso" },
                    { 4, "Falecido" },
                    { 1, "Ativo" },
                    { 2, "Aposentado" },
                    { 5, "INCORRETO" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoliticoContato_PoliticoId",
                table: "PoliticoContato",
                column: "PoliticoId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliticoContato_TipoId",
                table: "PoliticoContato",
                column: "TipoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MandatoSituacao");

            migrationBuilder.DropTable(
                name: "MandatoTipo");

            migrationBuilder.DropTable(
                name: "PoliticoContato");

            migrationBuilder.DropTable(
                name: "PoliticoEscolaridade");

            migrationBuilder.DropTable(
                name: "PoliticoSexo");

            migrationBuilder.DropTable(
                name: "PoliticoSituacao");

            migrationBuilder.DropTable(
                name: "Politicos");

            migrationBuilder.DropTable(
                name: "ContatoTipo");
        }
    }
}
