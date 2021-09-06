using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaMarketPDV.Models
{
    public class Produto
    {
        [Display(Name = "#")]
        public int Id { get; set; }

        [StringLength(13, MinimumLength = 1, ErrorMessage = "O código do produto dever ter entre 3 e 13 caracteres.")]
        [Required(ErrorMessage = "O código do produto é obrigatório!")]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "A descrição longa do produto dever ter entre 4 e 50 caracteres.")]
        [Required(ErrorMessage = "A descrição longa do produto é obrigatória!")] 
        [Display(Name = "Descrição Longa")]
        public string DescricaoLonga { get; set; }

        [StringLength(30, MinimumLength = 4, ErrorMessage = "A descrição curta do produto dever ter entre 4 e 30 caracteres.")]
        [Required(ErrorMessage = "A descrição curta do produto é obrigatória!")]
        [Display(Name = "Descrição Curta")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "A quantidade mínima do produto é obrigatória!")]
        [Display(Name = "Quant. Mínima")]
        [DisplayFormat(DataFormatString = "{0:F3}")]
        public double QuantMinima { get; set; }

        [Required(ErrorMessage = "O valor de venda do produto é obrigatório!")]
        [Display(Name = "Valor Venda")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double ValorVenda { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [StringLength(100, ErrorMessage = "As observações devem ter até 100 caracteres.")]
        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.Upload)]
        public string Foto { get; set; }

        [NotMapped]
        public IFormFile FotoProduto { get; set; }

        [Display(Name = "Data/Hora Cadastro")]
        public DateTime DataHoraCadastro { get; set; }

        public UnidadeMedida UnidadeMedida { get; set; }

        [Required(ErrorMessage = "A unidade de medida do produto é obrigatória!")]
        [Display(Name = "Unidade de Medida")]
        public int UnidadeMedidaId { get; set; }

        public Categoria Categoria { get; set; }
        
        [Required(ErrorMessage = "A categoria do produto é obrigatória!")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [Display(Name = "Estoques")]
        public ICollection<Estoque> Estoques { get; set; } = new List<Estoque>();

        public Produto() 
        { 
        }

        public Produto(int id, string codigo, string descricaoLonga, string descricaoCurta, 
                       double quantMinima, double valorVenda, bool ativo, string observacoes, 
                       string foto, DateTime dataHoraCadastro, UnidadeMedida unidadeMedida, 
                       Categoria categoria, IFormFile fotoProduto)
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
            this.FotoProduto = fotoProduto;
        }

        public void AdicionarEstoque(Estoque e)
        {
            Estoques.Add(e);
        }

        public void RemoverEstoque(Estoque e)
        {
            Estoques.Remove(e);
        }
    }
}
