using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models.Enums
{
    public enum TipoPessoa : int
    {
        [Display(Name = "Pessoa Jurídica")]
        JURIDICA = 1,
        [Display(Name = "Pessoa Física")]
        FISICA = 2
    }
}
