using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaMarketPDV.Migrations
{
    public partial class Inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: true),
                    DescricaoLonga = table.Column<string>(nullable: true),
                    DescricaoCurta = table.Column<string>(nullable: true),
                    QuantMinima = table.Column<double>(nullable: false),
                    ValorVenda = table.Column<double>(nullable: false),
                    FlgAtivo = table.Column<bool>(nullable: false),
                    Observacoes = table.Column<string>(nullable: true),
                    Foto = table.Column<string>(nullable: true),
                    DataHoraCadastro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produto");
        }
    }
}
