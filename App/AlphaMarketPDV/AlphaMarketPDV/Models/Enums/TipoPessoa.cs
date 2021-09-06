using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models.Enums
{
    public enum TipoPessoa : int
    {
        [Display(Name = "Jurídica")]
        JURIDICA = 1,
        [Display(Name = "Física")]
        FISICA = 2
    }
}
