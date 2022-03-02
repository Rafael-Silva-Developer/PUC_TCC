using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class ItemEntradaEstoque
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "#")]
        public int NrSeq { get; set; }

        [Display(Name = "Quantidade")]
        public double Qtd { get; set; }

        [Display(Name = "Valor Unitário")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double ValorUnitario { get; set; }

        [Display(Name = "Valor Item")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
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
