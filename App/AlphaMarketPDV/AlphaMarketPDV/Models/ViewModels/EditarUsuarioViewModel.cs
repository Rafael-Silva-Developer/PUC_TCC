using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models.ViewModels
{
    public class EditarUsuarioViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O nome email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        public UsuarioApp Usuario { get; set; }

        public ICollection<Loja> ListaLojas { get; set; }    
    }
}
