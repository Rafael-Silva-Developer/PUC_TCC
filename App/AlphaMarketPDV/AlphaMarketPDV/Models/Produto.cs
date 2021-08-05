using System;

namespace AlphaMarketPDV.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string DescricaoLonga { get; set; }
        public string DescricaoCurta { get; set; }
        public double QuantMinima { get; set; }
        public double ValorVenda { get; set; }
        public bool Ativo { get; set; }
        public string Observacoes { get; set; }
        public string Foto { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public UnidadeMedida UnidMedida { get; set; }
        public int UnidadeMedidaId { get; set; }
        public Categoria CategProd { get; set; }
        public int CategoriaId { get; set; }
        public Estoque Estoque { get; set; }
        public int EstoqueId { get; set; }

        public Produto() 
        { 
        }

        public Produto(int id, string codigo, string descricaoLonga, string descricaoCurta, 
                       double quantMinima, double valorVenda, bool ativo, string observacoes, 
                       string foto, DateTime dataHoraCadastro, UnidadeMedida unidMedida, Categoria categProd, Estoque estoque)
        {
            this.Id = id;
            this.Codigo = codigo;
            this.DescricaoLonga = descricaoLonga;
            this.DescricaoCurta = descricaoCurta;
            this.QuantMinima = quantMinima;
            this.ValorVenda = valorVenda;
            this.Ativo = ativo;
            this.Observacoes = observacoes;
            this.Foto = foto;
            this.DataHoraCadastro = dataHoraCadastro;
            this.UnidMedida = unidMedida;
            this.CategProd = categProd;
            this.Estoque = estoque;
        }

        public void AdicionarQtdEstoque(double qtd) 
        {
            Estoque.Saldo += qtd;
        }

        public void RetirarQtdEstoque(double qtd) 
        {
            Estoque.Saldo -= qtd;
        }
    }
}
