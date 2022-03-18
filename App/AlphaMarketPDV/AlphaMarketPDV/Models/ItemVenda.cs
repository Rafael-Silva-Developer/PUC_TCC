using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class ItemVenda
    {
        [Display(Name = "Id")]
        public int Id { get; private set; }

        [Display(Name = "NRSEQ")]
        public int NrSeq { get; set; }

        [Display(Name = "Valor Unitário")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double ValorUnitario { get; set; }

        [Display(Name = "Quantidade")]
        [DisplayFormat(DataFormatString = "{0:F3}")]
        public double Qtd { get; set; }

        [Display(Name = "Sub-Total")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double ValorItem { get; set; }

        [Display(Name = "Cancelado")]
        public bool Cancelado { get; set; }

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
            Id = id;
            NrSeq = nrSeq;
            ValorUnitario = valorUnitario;
            Qtd = qtd;
            ValorItem = valorItem;
            Cancelado = cancelado;
            Produto = produto;
            Venda = venda;
        }    
    }
}
