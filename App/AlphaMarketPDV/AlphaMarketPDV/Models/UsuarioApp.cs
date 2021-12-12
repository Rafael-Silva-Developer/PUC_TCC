using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace AlphaMarketPDV.Models
{
    public class UsuarioApp : IdentityUser
    {
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O nome do usuário dever ter entre 4 e 100 caracteres.")]
        [Required(ErrorMessage = "O nome do usuário é obrigatório!")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.Upload)]
        public string FotoUsuario { get; set; }

        [NotMapped]
        public IFormFile FotoUsuarioLoad { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        public bool CadastroLocal { get; set; }

        public Loja Loja { get; set; }

        [Required(ErrorMessage = "A loja é obrigatória!")]
        [Display(Name = "Loja")]
        public int LojaId { get; set; }

        public UsuarioApp() 
        { 
        }

        public UsuarioApp(string nome, 
                          string fotoUsuario, 
                          IFormFile fotoUsuarioLoad, 
                          bool ativo,
                          bool cadastroLocal,
                          Loja loja) : base() 
        {
            Nome = nome;
            FotoUsuario = fotoUsuario;
            FotoUsuarioLoad = fotoUsuarioLoad;
            Ativo = ativo;
            Loja = loja;
            CadastroLocal = cadastroLocal;
        }
    }
}
