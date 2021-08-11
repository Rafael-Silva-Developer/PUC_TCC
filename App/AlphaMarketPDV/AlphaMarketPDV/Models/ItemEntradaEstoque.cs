using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class ItemEntradaEstoque
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "NRSEQ")]
        public int NrSeq { get; set; }

        [Display(Name = "Quantidade")]
        public double Qtd { get; set; }

        [Display(Name = "Valor Unitário")]
        public double ValorUnitario { get; set; }

        [Display(Name = "Valor Item")]
        public double ValorItem { get; set; }

        public EntradaEstoque EntradaEstoque { get; set; }

        [Display(Name = "Entrada")]
        public int EntradaEstoqueId { get; set; }

        public Produto Produto { get; set; }

        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }

        public ItemEntradaEstoque() 
        { 
        }

        public ItemEntradaEstoque(int id, int nrSeq, double qtd, double valorUnitario, 
                                  double valorItem, EntradaEstoque entradaEstoque, Produto produto)
        {
            this.Id = id;
            this.NrSeq = nrSeq;
            this.Qtd = qtd;
            this.ValorUnitario = valorUnitario;
            this.ValorItem = valorItem;
            this.EntradaEstoque = entradaEstoque;
            this.Produto = produto;
        }

       

        
    }
}
