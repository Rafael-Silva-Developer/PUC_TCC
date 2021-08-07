using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public TipoUsuario Tipo { get; set; }

        public Usuario() 
        { 
        }

        public Usuario(int id, string login, string senha, bool ativo, string nome, TipoUsuario tipo)
        {
            this.Id = id;
            this.Login = login;
            this.Senha = senha;
            this.Ativo = ativo;
            this.Nome = nome;
            this.Tipo = tipo;
        }
    }
}
