using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaMarketPDV.Migrations
{
    public partial class Atualizacao_DB_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caixa_Usuario_UsuarioId",
                table: "Caixa");

            migrationBuilder.DropForeignKey(
                name: "FK_CaixaPagamento_Caixa_CaixaId",
                table: "CaixaPagamento");

            migrationBuilder.DropForeignKey(
                name: "FK_CaixaPagamento_FormaPagamento_FormaPagamentoId",
                table: "CaixaPagamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Contato_Fornecedor_FornecedorId",
                table: "Contato");

            migrationBuilder.DropForeignKey(
                name: "FK_EntradaEstoque_Fornecedor_FornecedorId",
                table: "EntradaEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_EntradaEstoque_Usuario_UsuarioId",
                table: "EntradaEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedor_Endereco_EnderecoId",
                table: "Fornecedor");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemEntradaEstoque_EntradaEstoque_EntradaEstoqueId",
                table: "ItemEntradaEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemEntradaEstoque_Produto_ProdutoId",
                table: "ItemEntradaEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaidaEstoque_Produto_ProdutoId",
                table: "ItemSaidaEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaidaEstoque_SaidaEstoque_SaidaEstoqueId",
                table: "ItemSaidaEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemVenda_Produto_ProdutoId",
                table: "ItemVenda");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemVenda_Venda_VendaId",
                table: "ItemVenda");

            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Estoque_EstoqueId",
                table: "Produto");

            migrationBuilder.DropForeignKey(
                name: "FK_SaidaEstoque_Usuario_UsuarioId",
                table: "SaidaEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Caixa_CaixaId",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Produto_EstoqueId",
                table: "Produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estoque",
                table: "Estoque");

            migrationBuilder.DropColumn(
                name: "EstoqueId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Estoque");

            migrationBuilder.AlterColumn<int>(
                name: "CaixaId",
                table: "Venda",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Usuario",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Usuario",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Usuario",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LojaId",
                table: "Usuario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Sigla",
                table: "UnidadeMedida",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "SaidaEstoque",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Justificativa",
                table: "SaidaEstoque",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "Produto",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoLonga",
                table: "Produto",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoCurta",
                table: "Produto",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "VendaId",
                table: "ItemVenda",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "ItemVenda",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SaidaEstoqueId",
                table: "ItemSaidaEstoque",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "ItemSaidaEstoque",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "ItemEntradaEstoque",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EntradaEstoqueId",
                table: "ItemEntradaEstoque",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Site",
                table: "Fornecedor",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RazaoSocial",
                table: "Fornecedor",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "Fornecedor",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumDocumento",
                table: "Fornecedor",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeRepresentante",
                table: "Fornecedor",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeFantasia",
                table: "Fornecedor",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InscrMunicipal",
                table: "Fornecedor",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InscrEstadual",
                table: "Fornecedor",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EnderecoId",
                table: "Fornecedor",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EndNumero",
                table: "Fornecedor",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EndComplemento",
                table: "Fornecedor",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "FormaPagamento",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LojaId",
                table: "Estoque",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "Estoque",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "EntradaEstoque",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "EntradaEstoque",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FornecedorId",
                table: "EntradaEstoque",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FornecedorId",
                table: "Contato",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FormaPagamentoId",
                table: "CaixaPagamento",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CaixaId",
                table: "CaixaPagamento",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Caixa",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estoque",
                table: "Estoque",
                columns: new[] { "LojaId", "ProdutoId" });

            migrationBuilder.CreateTable(
                name: "Loja",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 30, nullable: false),
                    EndComplemento = table.Column<string>(maxLength: 20, nullable: true),
                    EndNumero = table.Column<string>(maxLength: 8, nullable: true),
                    EnderecoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loja_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_LojaId",
                table: "Usuario",
                column: "LojaId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoque_ProdutoId",
                table: "Estoque",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Loja_EnderecoId",
                table: "Loja",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Caixa_Usuario_UsuarioId",
                table: "Caixa",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CaixaPagamento_Caixa_CaixaId",
                table: "CaixaPagamento",
                column: "CaixaId",
                principalTable: "Caixa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CaixaPagamento_FormaPagamento_FormaPagamentoId",
                table: "CaixaPagamento",
                column: "FormaPagamentoId",
                principalTable: "FormaPagamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_Fornecedor_FornecedorId",
                table: "Contato",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntradaEstoque_Fornecedor_FornecedorId",
                table: "EntradaEstoque",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntradaEstoque_Usuario_UsuarioId",
                table: "EntradaEstoque",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estoque_Loja_LojaId",
                table: "Estoque",
                column: "LojaId",
                principalTable: "Loja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estoque_Produto_ProdutoId",
                table: "Estoque",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedor_Endereco_EnderecoId",
                table: "Fornecedor",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemEntradaEstoque_EntradaEstoque_EntradaEstoqueId",
                table: "ItemEntradaEstoque",
                column: "EntradaEstoqueId",
                principalTable: "EntradaEstoque",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemEntradaEstoque_Produto_ProdutoId",
                table: "ItemEntradaEstoque",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSaidaEstoque_Produto_ProdutoId",
                table: "ItemSaidaEstoque",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSaidaEstoque_SaidaEstoque_SaidaEstoqueId",
                table: "ItemSaidaEstoque",
                column: "SaidaEstoqueId",
                principalTable: "SaidaEstoque",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemVenda_Produto_ProdutoId",
                table: "ItemVenda",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemVenda_Venda_VendaId",
                table: "ItemVenda",
                column: "VendaId",
                principalTable: "Venda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaidaEstoque_Usuario_UsuarioId",
                table: "SaidaEstoque",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Loja_LojaId",
                table: "Usuario",
                column: "LojaId",
                principalTable: "Loja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Caixa_CaixaId",
                table: "Venda",
                column: "CaixaId",
                principalTable: "Caixa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caixa_Usuario_UsuarioId",
                table: "Caixa");

            migrationBuilder.DropForeignKey(
                name: "FK_CaixaPagamento_Caixa_CaixaId",
                table: "CaixaPagamento");

            migrationBuilder.DropForeignKey(
                name: "FK_CaixaPagamento_FormaPagamento_FormaPagamentoId",
                table: "CaixaPagamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Contato_Fornecedor_FornecedorId",
                table: "Contato");

            migrationBuilder.DropForeignKey(
                name: "FK_EntradaEstoque_Fornecedor_FornecedorId",
                table: "EntradaEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_EntradaEstoque_Usuario_UsuarioId",
                table: "EntradaEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Loja_LojaId",
                table: "Estoque");

            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Produto_ProdutoId",
                table: "Estoque");

            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedor_Endereco_EnderecoId",
                table: "Fornecedor");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemEntradaEstoque_EntradaEstoque_EntradaEstoqueId",
                table: "ItemEntradaEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemEntradaEstoque_Produto_ProdutoId",
                table: "ItemEntradaEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaidaEstoque_Produto_ProdutoId",
                table: "ItemSaidaEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaidaEstoque_SaidaEstoque_SaidaEstoqueId",
                table: "ItemSaidaEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemVenda_Produto_ProdutoId",
                table: "ItemVenda");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemVenda_Venda_VendaId",
                table: "ItemVenda");

            migrationBuilder.DropForeignKey(
                name: "FK_SaidaEstoque_Usuario_UsuarioId",
                table: "SaidaEstoque");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Loja_LojaId",
                table: "Usuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Caixa_CaixaId",
                table: "Venda");

            migrationBuilder.DropTable(
                name: "Loja");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_LojaId",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estoque",
                table: "Estoque");

            migrationBuilder.DropIndex(
                name: "IX_Estoque_ProdutoId",
                table: "Estoque");

            migrationBuilder.DropColumn(
                name: "LojaId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "LojaId",
                table: "Estoque");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Estoque");

            migrationBuilder.AlterColumn<int>(
                name: "CaixaId",
                table: "Venda",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Usuario",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Usuario",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Usuario",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Sigla",
                table: "UnidadeMedida",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "SaidaEstoque",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Justificativa",
                table: "SaidaEstoque",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "Produto",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoLonga",
                table: "Produto",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DescricaoCurta",
                table: "Produto",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AddColumn<int>(
                name: "EstoqueId",
                table: "Produto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "VendaId",
                table: "ItemVenda",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "ItemVenda",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SaidaEstoqueId",
                table: "ItemSaidaEstoque",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "ItemSaidaEstoque",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "ItemEntradaEstoque",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EntradaEstoqueId",
                table: "ItemEntradaEstoque",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Site",
                table: "Fornecedor",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RazaoSocial",
                table: "Fornecedor",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "Fornecedor",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumDocumento",
                table: "Fornecedor",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "NomeRepresentante",
                table: "Fornecedor",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeFantasia",
                table: "Fornecedor",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "InscrMunicipal",
                table: "Fornecedor",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InscrEstadual",
                table: "Fornecedor",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EnderecoId",
                table: "Fornecedor",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "EndNumero",
                table: "Fornecedor",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EndComplemento",
                table: "Fornecedor",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "FormaPagamento",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Estoque",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "EntradaEstoque",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "EntradaEstoque",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FornecedorId",
                table: "EntradaEstoque",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FornecedorId",
                table: "Contato",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FormaPagamentoId",
                table: "CaixaPagamento",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CaixaId",
                table: "CaixaPagamento",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Caixa",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estoque",
                table: "Estoque",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_EstoqueId",
                table: "Produto",
                column: "EstoqueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Caixa_Usuario_UsuarioId",
                table: "Caixa",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CaixaPagamento_Caixa_CaixaId",
                table: "CaixaPagamento",
                column: "CaixaId",
                principalTable: "Caixa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CaixaPagamento_FormaPagamento_FormaPagamentoId",
                table: "CaixaPagamento",
                column: "FormaPagamentoId",
                principalTable: "FormaPagamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_Fornecedor_FornecedorId",
                table: "Contato",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntradaEstoque_Fornecedor_FornecedorId",
                table: "EntradaEstoque",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntradaEstoque_Usuario_UsuarioId",
                table: "EntradaEstoque",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedor_Endereco_EnderecoId",
                table: "Fornecedor",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemEntradaEstoque_EntradaEstoque_EntradaEstoqueId",
                table: "ItemEntradaEstoque",
                column: "EntradaEstoqueId",
                principalTable: "EntradaEstoque",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemEntradaEstoque_Produto_ProdutoId",
                table: "ItemEntradaEstoque",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSaidaEstoque_Produto_ProdutoId",
                table: "ItemSaidaEstoque",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSaidaEstoque_SaidaEstoque_SaidaEstoqueId",
                table: "ItemSaidaEstoque",
                column: "SaidaEstoqueId",
                principalTable: "SaidaEstoque",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemVenda_Produto_ProdutoId",
                table: "ItemVenda",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemVenda_Venda_VendaId",
                table: "ItemVenda",
                column: "VendaId",
                principalTable: "Venda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Estoque_EstoqueId",
                table: "Produto",
                column: "EstoqueId",
                principalTable: "Estoque",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaidaEstoque_Usuario_UsuarioId",
                table: "SaidaEstoque",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Caixa_CaixaId",
                table: "Venda",
                column: "CaixaId",
                principalTable: "Caixa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
