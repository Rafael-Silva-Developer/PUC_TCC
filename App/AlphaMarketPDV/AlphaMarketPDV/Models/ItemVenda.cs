using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class ItemVenda
    {
        [Display(Name = "Id")]
        public int Id { get; private set; }

        [Display(Name = "NRSEQ")]
        public int NrSeq { get; private set; }

        [Display(Name = "Valor Unitário")]
        public double ValorUnitario { get; private set; }

        [Display(Name = "Quantidade")]
        public double Qtd { get; private set; }

        [Display(Name = "Sub-Total")]
        public double ValorItem { get; private set; }

        [Display(Name = "Cancelado")]
        public bool Cancelado { get; private set; }

        public Produto Produto { get; set; }

        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }

        public Venda Venda { get; set; }

        [Display(Name = "Venda")]
        public int VendaId { get; set; }

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
           // Produto.AdicionarQtdEstoque(Qtd);
        }

        public void DiminuirSaldo()
        {
            //Produto.RetirarQtdEstoque(Qtd);
        }

        public void CalcularSubTotal() 
        {
            ValorItem = (Qtd * ValorUnitario);
        }

    }
}
