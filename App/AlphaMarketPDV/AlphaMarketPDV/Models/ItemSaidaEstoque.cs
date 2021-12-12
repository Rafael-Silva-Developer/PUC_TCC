using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class ItemSaidaEstoque
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "NRSEQ")]
        public int NrSeq { get; set; }

        [Display(Name = "Quantidade")]
        public double Qtd { get; set; }

        public Produto Produto { get; set; }

        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }

        public SaidaEstoque SaidaEstoque { get; set; }

        [Display(Name = "Saída")]
        public int SaidaEstoqueId { get; set; }

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
           // Produto.AdicionarQtdEstoque(Qtd);
        }

        public void DiminuirSaldo()
        {
            //Produto.RetirarQtdEstoque(Qtd);
        }
    }
}
