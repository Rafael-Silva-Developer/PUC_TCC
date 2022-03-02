using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaMarketPDV.Migrations
{
    public partial class atualizacao_db_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaidaEstoque_AspNetUsers_UsuarioId1",
                table: "SaidaEstoque");

            migrationBuilder.DropIndex(
                name: "IX_SaidaEstoque_UsuarioId1",
                table: "SaidaEstoque");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "SaidaEstoque");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "SaidaEstoque",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_SaidaEstoque_UsuarioId",
                table: "SaidaEstoque",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaidaEstoque_AspNetUsers_UsuarioId",
                table: "SaidaEstoque",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaidaEstoque_AspNetUsers_UsuarioId",
                table: "SaidaEstoque");

            migrationBuilder.DropIndex(
                name: "IX_SaidaEstoque_UsuarioId",
                table: "SaidaEstoque");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "SaidaEstoque",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "SaidaEstoque",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaidaEstoque_UsuarioId1",
                table: "SaidaEstoque",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SaidaEstoque_AspNetUsers_UsuarioId1",
                table: "SaidaEstoque",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
