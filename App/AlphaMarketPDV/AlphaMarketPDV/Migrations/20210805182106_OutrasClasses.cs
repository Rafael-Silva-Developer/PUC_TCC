using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaMarketPDV.Migrations
{
    public partial class OutrasClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FlgAtivo",
                table: "Produto",
                newName: "Ativo");

            migrationBuilder.AddColumn<int>(
                name: "CategProdId",
                table: "Produto",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstoqueId",
                table: "Produto",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnidMedidaId",
                table: "Produto",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cep = table.Column<string>(nullable: true),
                    Lougradouro = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Uf = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estoque",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Saldo = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoque", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormaPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    GeraTroco = table.Column<bool>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeMedida",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true),
                    Sigla = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeMedida", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RazaoSocial = table.Column<string>(nullable: true),
                    NomeFantasia = table.Column<string>(nullable: true),
                    NomeRepresentante = table.Column<string>(nullable: true),
                    EndComplemento = table.Column<string>(nullable: true),
                    EndNumero = table.Column<string>(nullable: true),
                    Site = table.Column<string>(nullable: true),
                    TipoEmpresa = table.Column<int>(nullable: false),
                    NumDocumento = table.Column<string>(nullable: true),
                    InscrEstadual = table.Column<string>(nullable: true),
                    InscrMunicipal = table.Column<string>(nullable: true),
                    Observacoes = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    EnderecoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fornecedor_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Caixa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataHora = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    TipoOperacao = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caixa_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaidaEstoque",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataHora = table.Column<DateTime>(nullable: false),
                    Justificativa = table.Column<string>(nullable: true),
                    DataHoraInformada = table.Column<DateTime>(nullable: false),
                    ValorTotal = table.Column<double>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaidaEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaidaEstoque_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contato",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Telefone = table.Column<string>(nullable: true),
                    Ramal = table.Column<int>(nullable: false),
                    Celular = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FornecedorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contato_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntradaEstoque",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataHora = table.Column<DateTime>(nullable: false),
                    ValorTotal = table.Column<double>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    DataHoraInformada = table.Column<DateTime>(nullable: false),
                    FornecedorId = table.Column<int>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradaEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntradaEstoque_Fornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntradaEstoque_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CaixaPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NrSeq = table.Column<int>(nullable: false),
                    ValorPago = table.Column<double>(nullable: false),
                    ValoTroco = table.Column<double>(nullable: false),
                    CaixaId = table.Column<int>(nullable: true),
                    FormaPagamentoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaixaPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaixaPagamento_Caixa_CaixaId",
                        column: x => x.CaixaId,
                        principalTable: "Caixa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CaixaPagamento_FormaPagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataHora = table.Column<DateTime>(nullable: false),
                    ValorTotal = table.Column<double>(nullable: false),
                    ValorDesconto = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CaixaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Venda_Caixa_CaixaId",
                        column: x => x.CaixaId,
                        principalTable: "Caixa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemSaidaEstoque",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NrSeq = table.Column<int>(nullable: false),
                    Qtd = table.Column<double>(nullable: false),
                    ProdutoId = table.Column<int>(nullable: true),
                    SaidaEstoqueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSaidaEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemSaidaEstoque_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemSaidaEstoque_SaidaEstoque_SaidaEstoqueId",
                        column: x => x.SaidaEstoqueId,
                        principalTable: "SaidaEstoque",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemEntradaEstoque",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NrSeq = table.Column<int>(nullable: false),
                    Qtd = table.Column<double>(nullable: false),
                    ValorUnitario = table.Column<double>(nullable: false),
                    ValorItem = table.Column<double>(nullable: false),
                    EntradaEstoqueId = table.Column<int>(nullable: true),
                    ProdutoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemEntradaEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemEntradaEstoque_EntradaEstoque_EntradaEstoqueId",
                        column: x => x.EntradaEstoqueId,
                        principalTable: "EntradaEstoque",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemEntradaEstoque_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemVenda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NrSeq = table.Column<int>(nullable: false),
                    ValorUnitario = table.Column<double>(nullable: false),
                    Qtd = table.Column<double>(nullable: false),
                    ValorItem = table.Column<double>(nullable: false),
                    Cancelado = table.Column<bool>(nullable: false),
                    ProdutoId = table.Column<int>(nullable: true),
                    VendaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemVenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemVenda_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemVenda_Venda_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Venda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CategProdId",
                table: "Produto",
                column: "CategProdId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_EstoqueId",
                table: "Produto",
                column: "EstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_UnidMedidaId",
                table: "Produto",
                column: "UnidMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_UsuarioId",
                table: "Caixa",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CaixaPagamento_CaixaId",
                table: "CaixaPagamento",
                column: "CaixaId");

            migrationBuilder.CreateIndex(
                name: "IX_CaixaPagamento_FormaPagamentoId",
                table: "CaixaPagamento",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contato_FornecedorId",
                table: "Contato",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaEstoque_FornecedorId",
                table: "EntradaEstoque",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaEstoque_UsuarioId",
                table: "EntradaEstoque",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_EnderecoId",
                table: "Fornecedor",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemEntradaEstoque_EntradaEstoqueId",
                table: "ItemEntradaEstoque",
                column: "EntradaEstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemEntradaEstoque_ProdutoId",
                table: "ItemEntradaEstoque",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSaidaEstoque_ProdutoId",
                table: "ItemSaidaEstoque",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSaidaEstoque_SaidaEstoqueId",
                table: "ItemSaidaEstoque",
                column: "SaidaEstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemVenda_ProdutoId",
                table: "ItemVenda",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemVenda_VendaId",
                table: "ItemVenda",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_SaidaEstoque_UsuarioId",
                table: "SaidaEstoque",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Venda_CaixaId",
                table: "Venda",
                column: "CaixaId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "CaixaPagamento");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Contato");

            migrationBuilder.DropTable(
                name: "Estoque");

            migrationBuilder.DropTable(
                name: "ItemEntradaEstoque");

            migrationBuilder.DropTable(
                name: "ItemSaidaEstoque");

            migrationBuilder.DropTable(
                name: "ItemVenda");

            migrationBuilder.DropTable(
                name: "UnidadeMedida");

            migrationBuilder.DropTable(
                name: "FormaPagamento");

            migrationBuilder.DropTable(
                name: "EntradaEstoque");

            migrationBuilder.DropTable(
                name: "SaidaEstoque");

            migrationBuilder.DropTable(
                name: "Venda");

            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "Caixa");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Produto_CategProdId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_EstoqueId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_UnidMedidaId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "CategProdId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "EstoqueId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "UnidMedidaId",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Produto",
                newName: "FlgAtivo");
        }
    }
}
