using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models.Enums
{
    public enum TipoUsuario : int
    {
        [Display(Name = "Supervisor")]
        SUPERVISOR = 1,
        [Display(Name = "Atendente")]
        ATENDENTE = 2
    }
}
