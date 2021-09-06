﻿// <auto-generated />
using System;
using AlphaMarketPDV.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AlphaMarketPDV.Migrations
{
    [DbContext(typeof(AlphaMarketPDVContext))]
    [Migration("20210826001049_Atualizacao_Base_4")]
    partial class Atualizacao_Base_4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AlphaMarketPDV.Models.Caixa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataHora");

                    b.Property<int>("TipoOperacao");

                    b.Property<int>("UsuarioId");

                    b.Property<double>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Caixa");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.CaixaPagamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CaixaId");

                    b.Property<int>("FormaPagamentoId");

                    b.Property<int>("NrSeq");

                    b.Property<double>("ValoTroco");

                    b.Property<double>("ValorPago");

                    b.HasKey("Id");

                    b.HasIndex("CaixaId");

                    b.HasIndex("FormaPagamentoId");

                    b.ToTable("CaixaPagamento");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Contato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Celular");

                    b.Property<string>("Email");

                    b.Property<int>("FornecedorId");

                    b.Property<int>("Ramal");

                    b.Property<string>("Telefone");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("Contato");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bairro");

                    b.Property<string>("Cep");

                    b.Property<string>("Cidade");

                    b.Property<string>("Lougradouro");

                    b.Property<string>("Uf");

                    b.HasKey("Id");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.EntradaEstoque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataHora");

                    b.Property<DateTime>("DataHoraInformada");

                    b.Property<int>("FornecedorId");

                    b.Property<string>("Observacao")
                        .HasMaxLength(100);

                    b.Property<int>("UsuarioId");

                    b.Property<double>("ValorTotal");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("EntradaEstoque");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Estoque", b =>
                {
                    b.Property<int>("LojaId");

                    b.Property<int>("ProdutoId");

                    b.Property<double>("Saldo");

                    b.Property<int>("Status");

                    b.HasKey("LojaId", "ProdutoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Estoque");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.FormaPagamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<bool>("GeraTroco");

                    b.HasKey("Id");

                    b.ToTable("FormaPagamento");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Fornecedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<string>("EndComplemento")
                        .HasMaxLength(20);

                    b.Property<string>("EndNumero")
                        .HasMaxLength(8);

                    b.Property<int>("EnderecoId");

                    b.Property<string>("InscrEstadual")
                        .HasMaxLength(30);

                    b.Property<string>("InscrMunicipal")
                        .HasMaxLength(30);

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("NomeRepresentante")
                        .HasMaxLength(60);

                    b.Property<string>("NumDocumento")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("Observacoes")
                        .HasMaxLength(100);

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Site")
                        .HasMaxLength(60);

                    b.Property<int>("TipoEmpresa");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Fornecedor");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.ItemEntradaEstoque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EntradaEstoqueId");

                    b.Property<int>("NrSeq");

                    b.Property<int>("ProdutoId");

                    b.Property<double>("Qtd");

                    b.Property<double>("ValorItem");

                    b.Property<double>("ValorUnitario");

                    b.HasKey("Id");

                    b.HasIndex("EntradaEstoqueId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ItemEntradaEstoque");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.ItemSaidaEstoque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NrSeq");

                    b.Property<int>("ProdutoId");

                    b.Property<double>("Qtd");

                    b.Property<int>("SaidaEstoqueId");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("SaidaEstoqueId");

                    b.ToTable("ItemSaidaEstoque");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.ItemVenda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Cancelado");

                    b.Property<int>("NrSeq");

                    b.Property<int>("ProdutoId");

                    b.Property<double>("Qtd");

                    b.Property<double>("ValorItem");

                    b.Property<double>("ValorUnitario");

                    b.Property<int>("VendaId");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("VendaId");

                    b.ToTable("ItemVenda");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Loja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("EndComplemento")
                        .HasMaxLength(20);

                    b.Property<string>("EndNumero")
                        .HasMaxLength(8);

                    b.Property<int>("EnderecoId");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Loja");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<int>("CategoriaId");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(13);

                    b.Property<DateTime>("DataHoraCadastro");

                    b.Property<string>("DescricaoCurta")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("DescricaoLonga")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Foto");

                    b.Property<string>("Observacoes")
                        .HasMaxLength(100);

                    b.Property<double>("QuantMinima");

                    b.Property<int>("UnidadeMedidaId");

                    b.Property<double>("ValorVenda");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("UnidadeMedidaId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.SaidaEstoque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataHora");

                    b.Property<DateTime>("DataHoraInformada");

                    b.Property<string>("Justificativa")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<int>("UsuarioId");

                    b.Property<double>("ValorTotal");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("SaidaEstoque");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.UnidadeMedida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.HasKey("Id");

                    b.ToTable("UnidadeMedida");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<string>("FotoUsuario");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<int>("LojaId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("Tipo");

                    b.HasKey("Id");

                    b.HasIndex("LojaId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Venda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CaixaId");

                    b.Property<DateTime>("DataHora");

                    b.Property<int>("Status");

                    b.Property<double>("ValorDesconto");

                    b.Property<double>("ValorTotal");

                    b.HasKey("Id");

                    b.HasIndex("CaixaId");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Caixa", b =>
                {
                    b.HasOne("AlphaMarketPDV.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.CaixaPagamento", b =>
                {
                    b.HasOne("AlphaMarketPDV.Models.Caixa", "Caixa")
                        .WithMany("CaixaPagamentos")
                        .HasForeignKey("CaixaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlphaMarketPDV.Models.FormaPagamento", "FormaPagamento")
                        .WithMany()
                        .HasForeignKey("FormaPagamentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Contato", b =>
                {
                    b.HasOne("AlphaMarketPDV.Models.Fornecedor", "Fornecedor")
                        .WithMany("Contatos")
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.EntradaEstoque", b =>
                {
                    b.HasOne("AlphaMarketPDV.Models.Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlphaMarketPDV.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Estoque", b =>
                {
                    b.HasOne("AlphaMarketPDV.Models.Loja", "Loja")
                        .WithMany("Estoques")
                        .HasForeignKey("LojaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlphaMarketPDV.Models.Produto", "Produto")
                        .WithMany("Estoques")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Fornecedor", b =>
                {
                    b.HasOne("AlphaMarketPDV.Models.Endereco", "Endereco")
                        .WithMany("Fornecedores")
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.ItemEntradaEstoque", b =>
                {
                    b.HasOne("AlphaMarketPDV.Models.EntradaEstoque", "EntradaEstoque")
                        .WithMany("ItensEntradaEstoque")
                        .HasForeignKey("EntradaEstoqueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlphaMarketPDV.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.ItemSaidaEstoque", b =>
                {
                    b.HasOne("AlphaMarketPDV.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlphaMarketPDV.Models.SaidaEstoque", "SaidaEstoque")
                        .WithMany("ItensSaidaEstoque")
                        .HasForeignKey("SaidaEstoqueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.ItemVenda", b =>
                {
                    b.HasOne("AlphaMarketPDV.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlphaMarketPDV.Models.Venda", "Venda")
                        .WithMany("ItensVenda")
                        .HasForeignKey("VendaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Loja", b =>
                {
                    b.HasOne("AlphaMarketPDV.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Produto", b =>
                {
                    b.HasOne("AlphaMarketPDV.Models.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AlphaMarketPDV.Models.UnidadeMedida", "UnidadeMedida")
                        .WithMany("Produtos")
                        .HasForeignKey("UnidadeMedidaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.SaidaEstoque", b =>
                {
                    b.HasOne("AlphaMarketPDV.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Usuario", b =>
                {
                    b.HasOne("AlphaMarketPDV.Models.Loja", "Loja")
                        .WithMany("Usuarios")
                        .HasForeignKey("LojaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlphaMarketPDV.Models.Venda", b =>
                {
                    b.HasOne("AlphaMarketPDV.Models.Caixa", "Caixa")
                        .WithMany()
                        .HasForeignKey("CaixaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
