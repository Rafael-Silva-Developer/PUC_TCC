using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaMarketPDV.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
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
