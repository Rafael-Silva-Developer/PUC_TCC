using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class ItemVenda
    {
        public int Id { get; private set; }
        public int NrSeq { get; private set; }
        public double ValorUnitario { get; private set; }
        public double Qtd { get; private set; }
        public double ValorItem { get; private set; }
        public bool Cancelado { get; private set; }
        public Produto Produto { get; set; }
        public Venda Venda { get; set; }

        public ItemVenda() 
        { 
        }

        public ItemVenda(int id, int nrSeq, double valorUnitario, double qtd, double valorItem, 
                         bool cancelado, Produto produto, Venda venda)
        {
            this.Id = id;
            this.NrSeq = nrSeq;
            this.ValorUnitario = valorUnitario;
            this.Qtd = qtd;
            this.ValorItem = valorItem;
            this.Cancelado = cancelado;
            this.Produto = produto;
            this.Venda = venda;
        }

        public void AumentarSaldo()
        {
            Produto.AdicionarQtdEstoque(Qtd);
        }

        public void DiminuirSaldo()
        {
            Produto.RetirarQtdEstoque(Qtd);
        }

        public void CalcularSubTotal() 
        {
            ValorItem = (Qtd * ValorUnitario);
        }

    }
}
