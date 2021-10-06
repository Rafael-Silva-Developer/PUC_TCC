using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models.Infra
{
    public class RegistrarNovoUsuarioViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} precisa ter ao menos {2} e no máximo {1} caracteres de cumprimento.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "Os valores informados para SENHA e CONFIRMAÇÃO não são iguais.")]
        public string ConfirmPassword { get; set; }

        public UsuarioApp Usuario { get; set; }

        public ICollection<Loja> ListaLojas { get; set; }
    }
}
