using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaMarketPDV.Migrations
{
    public partial class ChaveEstrageiraProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Categoria_CategProdId",
                table: "Produto");

            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Estoque_EstoqueId",
                table: "Produto");

            migrationBuilder.DropForeignKey(
                name: "FK_Produto_UnidadeMedida_UnidMedidaId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_CategProdId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_UnidMedidaId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "CategProdId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "UnidMedidaId",
                table: "Produto");

            migrationBuilder.AlterColumn<int>(
                name: "EstoqueId",
                table: "Produto",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Produto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnidadeMedidaId",
                table: "Produto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CategoriaId",
                table: "Produto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_UnidadeMedidaId",
                table: "Produto",
                column: "UnidadeMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Categoria_CategoriaId",
                table: "Produto",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Estoque_EstoqueId",
                table: "Produto",
                column: "EstoqueId",
                principalTable: "Estoque",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_UnidadeMedida_UnidadeMedidaId",
                table: "Produto",
                column: "UnidadeMedidaId",
                principalTable: "UnidadeMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Categoria_CategoriaId",
                table: "Produto");

            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Estoque_EstoqueId",
                table: "Produto");

            migrationBuilder.DropForeignKey(
                name: "FK_Produto_UnidadeMedida_UnidadeMedidaId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_CategoriaId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_UnidadeMedidaId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "UnidadeMedidaId",
                table: "Produto");

            migrationBuilder.AlterColumn<int>(
                name: "EstoqueId",
                table: "Produto",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CategProdId",
                table: "Produto",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnidMedidaId",
                table: "Produto",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CategProdId",
                table: "Produto",
                column: "CategProdId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_UnidMedidaId",
                table: "Produto",
                column: "UnidMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Categoria_CategProdId",
                table: "Produto",
                column: "CategProdId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Estoque_EstoqueId",
                table: "Produto",
                column: "EstoqueId",
                principalTable: "Estoque",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_UnidadeMedida_UnidMedidaId",
                table: "Produto",
                column: "UnidMedidaId",
                principalTable: "UnidadeMedida",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
