using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models.ViewModels
{
    public class AlterarSenhaViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "A {0} precisa ter ao menos {2} e no máximo {1} caracteres de cumprimento.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NovaSenha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("NovaSenha", ErrorMessage = "Os valores informados para SENHA e CONFIRMAÇÃO não são iguais.")]
        public string ConfirmarNovaSenha { get; set; }

        public string Id { get; set; }
    }
}
