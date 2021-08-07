using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class Estoque
    {
        public int Id { get; set; }
        public double Saldo { get; set; }
        public StatusEstoque Status { get; set; }
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public Estoque() 
        { 
        }

        public Estoque(int id, double saldo, StatusEstoque status)
        {
            this.Id = id;
            this.Saldo = saldo;
            this.Status = status;
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
