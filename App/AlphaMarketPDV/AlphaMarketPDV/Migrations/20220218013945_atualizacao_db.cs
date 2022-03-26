using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaMarketPDV.Migrations
{
    public partial class atualizacao_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntradaEstoque_AspNetUsers_UsuarioId1",
                table: "EntradaEstoque");

            migrationBuilder.DropIndex(
                name: "IX_EntradaEstoque_UsuarioId1",
                table: "EntradaEstoque");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "EntradaEstoque");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "EntradaEstoque",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_EntradaEstoque_UsuarioId",
                table: "EntradaEstoque",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntradaEstoque_AspNetUsers_UsuarioId",
                table: "EntradaEstoque",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntradaEstoque_AspNetUsers_UsuarioId",
                table: "EntradaEstoque");

            migrationBuilder.DropIndex(
                name: "IX_EntradaEstoque_UsuarioId",
                table: "EntradaEstoque");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "EntradaEstoque",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "EntradaEstoque",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntradaEstoque_UsuarioId1",
                table: "EntradaEstoque",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EntradaEstoque_AspNetUsers_UsuarioId1",
                table: "EntradaEstoque",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
