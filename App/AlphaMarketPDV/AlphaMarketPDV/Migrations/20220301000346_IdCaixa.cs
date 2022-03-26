using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaMarketPDV.Migrations
{
    public partial class IdCaixa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caixa_AspNetUsers_UsuarioId1",
                table: "Caixa");

            migrationBuilder.DropIndex(
                name: "IX_Caixa_UsuarioId1",
                table: "Caixa");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Caixa");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Caixa",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_UsuarioId",
                table: "Caixa",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Caixa_AspNetUsers_UsuarioId",
                table: "Caixa",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caixa_AspNetUsers_UsuarioId",
                table: "Caixa");

            migrationBuilder.DropIndex(
                name: "IX_Caixa_UsuarioId",
                table: "Caixa");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Caixa",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Caixa",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_UsuarioId1",
                table: "Caixa",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Caixa_AspNetUsers_UsuarioId1",
                table: "Caixa",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
