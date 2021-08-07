using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição da categoria é obrigatória!")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "A descrição da categoria dever ter entre 4 e 30 caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public Categoria() 
        { 
        }

        public Categoria(int id, string descricao, bool ativo)
        {
            this.Id = id;
            this.Descricao = descricao;
            this.Ativo = ativo;
        }

        public void AdicionarProduto(Produto p) 
        {
            Produtos.Add(p);
        }

        public void RemoverProduto(Produto p) 
        {
            Produtos.Remove(p);
        }
    }
}
