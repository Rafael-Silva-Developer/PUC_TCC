using System;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models.ViewModels.FluxoCaixa
{
    public class FluxoCaixaGenericViewModel
    {
        [Required(ErrorMessage = "A data de operação é obrigatória!")]
        [Display(Name = "Data/Hora Operação")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "O valor da operação é obrigatório!")]
        [Display(Name = "Valor")]
        public double Valor { get; set; }
    }
}
