using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaMarketPDV.Migrations
{
    public partial class restrigindo_unidade_medida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_UnidadeMedida_UnidadeMedidaId",
                table: "Produto");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_UnidadeMedida_UnidadeMedidaId",
                table: "Produto",
                column: "UnidadeMedidaId",
                principalTable: "UnidadeMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_UnidadeMedida_UnidadeMedidaId",
                table: "Produto");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_UnidadeMedida_UnidadeMedidaId",
                table: "Produto",
                column: "UnidadeMedidaId",
                principalTable: "UnidadeMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
