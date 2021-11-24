using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models.ViewModels
{
    public class ContatoViewModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Mensagem { get; set; }
    }
}
