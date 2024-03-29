﻿using Microsoft.EntityFrameworkCore;
using AlphaMarketPDV.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AlphaMarketPDV.Data
{
    public class AlphaMarketPDVContext : IdentityDbContext<UsuarioApp, PerfilApp, string>
    {
        public AlphaMarketPDVContext(DbContextOptions<AlphaMarketPDVContext> options)
            : base(options)
        {
        }

        public DbSet<AlphaMarketPDV.Models.Caixa> Caixa { get; set; }
        public DbSet<AlphaMarketPDV.Models.CaixaPagamento> CaixaPagamento { get; set; }
        public DbSet<AlphaMarketPDV.Models.Categoria> Categoria { get; set; }
        public DbSet<AlphaMarketPDV.Models.Contato> Contato { get; set; }
        public DbSet<AlphaMarketPDV.Models.Endereco> Endereco { get; set; }
        public DbSet<AlphaMarketPDV.Models.EntradaEstoque> EntradaEstoque { get; set; }
        public DbSet<AlphaMarketPDV.Models.Estoque> Estoque { get; set; }
        public DbSet<AlphaMarketPDV.Models.FormaPagamento> FormaPagamento { get; set; }
        public DbSet<AlphaMarketPDV.Models.Fornecedor> Fornecedor { get; set; }
        public DbSet<AlphaMarketPDV.Models.ItemEntradaEstoque> ItemEntradaEstoque { get; set; }
        public DbSet<AlphaMarketPDV.Models.ItemSaidaEstoque> ItemSaidaEstoque { get; set; }
        public DbSet<AlphaMarketPDV.Models.ItemVenda> ItemVenda { get; set; }
        public DbSet<AlphaMarketPDV.Models.Produto> Produto { get; set; }
        public DbSet<AlphaMarketPDV.Models.SaidaEstoque> SaidaEstoque { get; set; }
        public DbSet<AlphaMarketPDV.Models.UnidadeMedida> UnidadeMedida { get; set; }
        public DbSet<AlphaMarketPDV.Models.Venda> Venda { get; set; }
        public DbSet<AlphaMarketPDV.Models.Loja> Loja { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Produtos)
                .WithOne(p => p.Categoria)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UnidadeMedida>()
                .HasMany(u => u.Produtos)
                .WithOne(p => p.UnidadeMedida)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Estoque>()
                .HasKey(e => new { e.LojaId, e.ProdutoId });

            modelBuilder.Entity<Estoque>()
                .HasOne(e => e.Produto)
                .WithMany(p => p.Estoques)
                .HasForeignKey(e => e.ProdutoId);

            modelBuilder.Entity<Estoque>()
                .HasOne(e => e.Loja)
                .WithMany(l => l.Estoques)
                .HasForeignKey(e => e.LojaId);        
        }
    }
}
