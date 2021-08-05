using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaMarketPDV.Models
{
    public class ItemSaidaEstoque
    {
        public int Id { get; set; }
        public int NrSeq { get; set; }
        public double Qtd { get; set; }
        public Produto Produto { get; set; }
        public SaidaEstoque SaidaEstoque { get; set; }

        public ItemSaidaEstoque() 
        { 
        }

        public ItemSaidaEstoque(int id, int nrSeq, double qtd, Produto produto, SaidaEstoque saidaEstoque)
        {
            this.Id = id;
            this.NrSeq = nrSeq;
            this.Qtd = qtd;
            this.Produto = produto;
            this.SaidaEstoque = saidaEstoque;
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
