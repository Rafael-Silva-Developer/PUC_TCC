using AlphaMarketPDV.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace AlphaMarketPDV.Models
{
    public class Usuario
    {
        [Display(Name = "#")]
        public int Id { get; set; }

        [StringLength(25, MinimumLength = 4, ErrorMessage = "O login do usuário dever ter entre 4 e 25 caracteres.")]
        [Required(ErrorMessage = "O login do usuário é obrigatório!")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [StringLength(32, MinimumLength = 4, ErrorMessage = "A senha do usuário dever ter entre 4 e 32 caracteres.")]
        [Required(ErrorMessage = "A senha do usuário é obrigatória!")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [StringLength(100, MinimumLength = 4, ErrorMessage = "O nome do usuário dever ter entre 4 e 100 caracteres.")]
        [Required(ErrorMessage = "O nome do usuário é obrigatório!")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O tipo de usuário é obrigatório!")]
        [Display(Name = "Tipo de Usuário")]
        public TipoPerfil Tipo { get; set; }

        public Loja Loja { get; set; }

        [Required(ErrorMessage = "A loja é obrigatória!")]
        [Display(Name = "Loja")]
        public int LojaId { get; set; }

        [Display(Name = "Imagem")]
        [DataType(DataType.Upload)]
        public string FotoUsuario { get; set; }

        [NotMapped]
        public IFormFile FotoUsuarioLoad { get; set; }


        public Usuario() 
        { 
        }

        public Usuario(int id, string login, string senha, bool ativo, string nome,
                       TipoPerfil tipo, Loja loja, string fotoUsuario, IFormFile fotoUsuarioLoad)
        {
            this.Id = id;
            this.Login = login;
            this.Senha = senha;
            this.Ativo = ativo;
            this.Nome = nome;
            this.Tipo = tipo;
            this.Loja = loja;
            this.FotoUsuario = fotoUsuario;
            this.FotoUsuarioLoad = fotoUsuarioLoad;
        }
    }
}
