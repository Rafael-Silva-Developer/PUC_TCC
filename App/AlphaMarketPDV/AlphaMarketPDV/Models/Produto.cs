using System;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O código do produto é obrigatório!")]
        [StringLength(13, MinimumLength = 4, ErrorMessage = "O código do produto dever ter entre 4 e 13 caracteres.")]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "A descrição longa do produto é obrigatória!")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "A descrição longa do produto dever ter entre 4 e 30 caracteres.")]
        [Display(Name = "Descrição Longa")]
        public string DescricaoLonga { get; set; }

        [Required(ErrorMessage = "A descrição curta do produto é obrigatória!")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "A descrição curta do produto dever ter entre 4 e 20 caracteres.")]
        [Display(Name = "Descrição Curta")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "A quantidade mínima do produto é obrigatória!")]
        [Display(Name = "Quant. Mínima")]
        [DisplayFormat(DataFormatString = "{0:F3}")]
        public double QuantMinima { get; set; }

        [Required(ErrorMessage = "O valro de venda do produto é obrigatório!")]
        [Display(Name = "Valor Venda")]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double ValorVenda { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.Upload)]
        public string Foto { get; set; }

        [Display(Name = "Data/Hora Cadastro")]
        public DateTime DataHoraCadastro { get; set; }

        public UnidadeMedida UnidadeMedida { get; set; }
        [Required(ErrorMessage = "A unidade de medida do produto é obrigatório!")]
        [Display(Name = "Unidade de Medida")]
        public int UnidadeMedidaId { get; set; }

        public Categoria Categoria { get; set; }
        [Required(ErrorMessage = "A categoria do produto é obrigatório!")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        
        public Estoque Estoque { get; set; }
        public int EstoqueId { get; set; }

        public Produto() 
        { 
        }

        public Produto(int id, string codigo, string descricaoLonga, string descricaoCurta, 
                       double quantMinima, double valorVenda, bool ativo, string observacoes, 
                       string foto, DateTime dataHoraCadastro, UnidadeMedida unidadeMedida, 
                       Categoria categoria, Estoque estoque)
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
            this.UnidadeMedida = unidadeMedida;
            this.Categoria = categoria;
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
