using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models.ViewModels.Estoque
{
    public class SaidaViewModel
    {
        public SaidaEstoque SaidaEstoque { get; set; }

        public Produto Produto { get; set; }

        [Display(Name = "Quantidade")]
        [DisplayFormat(DataFormatString = "{0:F3}")]
        public double Quantidade { get; set; }

        [Display(Name = "SubTotal")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double SubTotal { get; set; }

        [Display(Name = "Total")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double TotalSaida { get; set; }

        public SaidaViewModel()
        {
        }
    }
}
