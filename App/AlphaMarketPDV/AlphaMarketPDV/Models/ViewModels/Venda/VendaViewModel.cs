using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models.ViewModels.Venda
{
    public class VendaViewModel
    {
        public Produto Produto { get; set; }

        [Display(Name = "Quantidade")]
        [DisplayFormat(DataFormatString = "{0:F3}")]
        public double Quantidade { get; set; }

        [Display(Name = "SubTotal")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double SubTotal { get; set; }

        [Display(Name = "Total Venda")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double TotalVenda { get; set; }

        [Display(Name = "Desconto")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double Desconto { get; set; }

        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double TotalPagar { get; set; }

        public ICollection<FormaPagamento> ListaFormasPagamento { get; set; }

        public ICollection<ItemVenda> ItensVenda { get; set; } = new List<ItemVenda>();
        
        public ICollection<CaixaPagamento> ItemPagamento { get; set; } = new List<CaixaPagamento>();

        [Display(Name = "Forma de Pagamento")]
        public int FormaPagamentoId { get; set; }

        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double ValorFormaPag { get; set; }

        [Display(Name = "Valor Restante")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double ValorRestante { get; set; }

        public VendaViewModel() 
        { 
        }
    }
}