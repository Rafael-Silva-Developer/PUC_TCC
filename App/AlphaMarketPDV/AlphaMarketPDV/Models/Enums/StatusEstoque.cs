using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models.Enums
{
    public enum StatusEstoque : int
    {
        [Display(Name = "Baixo")]
        BAIXO = 1,
        [Display(Name = "Normal")]
        NORMAL = 2
    }
}
