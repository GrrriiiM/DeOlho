using Microsoft.EntityFrameworkCore.Migrations;

namespace DeOlho.ETL.dadosabertos_leg_br.Infrastructure.Migrations
{
    public partial class ajuste_notafiscal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscal_DeputadoFederal_DeputadoFederalId",
                table: "NotaFiscal");

            migrationBuilder.DropIndex(
                name: "IX_NotaFiscal_DeputadoFederalId",
                table: "NotaFiscal");

            migrationBuilder.RenameColumn(
                name: "DeputadoFederalId",
                table: "NotaFiscal",
                newName: "DeputadoFederalOrigemId");

            migrationBuilder.AddColumn<long>(
                name: "CPF",
                table: "NotaFiscal",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "NotaFiscal");

            migrationBuilder.RenameColumn(
                name: "DeputadoFederalOrigemId",
                table: "NotaFiscal",
                newName: "DeputadoFederalId");

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_DeputadoFederalId",
                table: "NotaFiscal",
                column: "DeputadoFederalId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotaFiscal_DeputadoFederal_DeputadoFederalId",
                table: "NotaFiscal",
                column: "DeputadoFederalId",
                principalTable: "DeputadoFederal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
