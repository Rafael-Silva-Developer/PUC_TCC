using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaMarketPDV.Migrations
{
    public partial class atualizacao_db_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentificadorRegistro",
                table: "SaidaEstoque",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Valor",
                table: "ItemSaidaEstoque",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorTotal",
                table: "ItemSaidaEstoque",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentificadorRegistro",
                table: "SaidaEstoque");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "ItemSaidaEstoque");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "ItemSaidaEstoque");
        }
    }
}
