using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models.Enums
{
    public enum TipoCaixa : int
    {
        [Display(Name = "Abertura")]
        ABERTURA = 1,
        [Display(Name = "Fechamento")]
        FECHAMENTO = 2,
        [Display(Name = "Sangria")]
        SANGRIA = 3,
        [Display(Name = "Venda")]
        VENDA = 4,
        [Display(Name = "Estorno")]
        ESTORNO = 5
    }
}
