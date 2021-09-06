using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models.Enums
{
    public enum StatusVenda : int
    {
        [Display(Name = "Cancelado")]
        CANCELADO = 1,
        [Display(Name = "Finalizado")]
        FINALIZADO = 2
    }
}
