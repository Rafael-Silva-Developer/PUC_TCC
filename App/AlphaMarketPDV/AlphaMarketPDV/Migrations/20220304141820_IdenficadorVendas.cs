using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaMarketPDV.Migrations
{
    public partial class IdenficadorVendas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentificadorRegistro",
                table: "Venda",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPagar",
                table: "Venda",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentificadorRegistro",
                table: "Venda");

            migrationBuilder.DropColumn(
                name: "TotalPagar",
                table: "Venda");
        }
    }
}
