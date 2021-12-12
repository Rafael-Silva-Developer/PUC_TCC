using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class UnidadeMedida
    {
        [Display(Name = "#")]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 4, ErrorMessage = "A descrição da unidade de medida dever ter entre 4 e 30 caracteres.")]
        [Required(ErrorMessage = "A descrição da unidade de medida é obrigatória!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "A sigla da unidade de medida dever ter 2 caracteres.")]
        [Required(ErrorMessage = "A sigla da unidade de medida é obrigatório!")]
        [Display(Name = "Sigla")]
        public string Sigla { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Produtos")]
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public UnidadeMedida()
        {
        }

        public UnidadeMedida(int id, string descricao, string sigla, bool ativo) 
        {
            this.Id = id;
            this.Descricao = descricao;
            this.Sigla = sigla;
            this.Ativo = ativo;
        }

        public void AdicionarProduto(Produto p) 
        {
            Produtos.Add(p);
        }

        public void RemoverProoduto(Produto p) 
        {
            Produtos.Remove(p);
        }
    }
}
