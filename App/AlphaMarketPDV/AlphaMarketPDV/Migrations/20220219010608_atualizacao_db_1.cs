using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaMarketPDV.Migrations
{
    public partial class atualizacao_db_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentificadorRegistro",
                table: "EntradaEstoque",
                maxLength: 32,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentificadorRegistro",
                table: "EntradaEstoque");
        }
    }
}
