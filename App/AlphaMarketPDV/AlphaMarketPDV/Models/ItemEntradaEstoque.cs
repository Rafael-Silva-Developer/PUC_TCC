using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaMarketPDV.Models
{
    public class ItemEntradaEstoque
    {
        public int Id { get; set; }
        public int NrSeq { get; set; }
        public double Qtd { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorItem { get; set; }
        public EntradaEstoque EntradaEstoque { get; set; }
        public Produto Produto { get; set; }

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

        public void AumentarSaldo() 
        {
            Produto.AdicionarQtdEstoque(Qtd);
        }

        public void DiminuirSaldo() 
        {
            Produto.RetirarQtdEstoque(Qtd);
        }
    }
}
