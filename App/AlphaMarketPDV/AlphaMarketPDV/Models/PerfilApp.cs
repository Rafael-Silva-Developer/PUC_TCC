using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System;
using AlphaMarketPDV.Models.Enums;

namespace AlphaMarketPDV.Models
{
    public class PerfilApp : IdentityRole
    {
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public TipoPerfil TipoPerfil { get; set; }

        public PerfilApp() : base()
        {
        }

        public PerfilApp(string roleName) : base(roleName) 
        { 
        }

        public PerfilApp(string roleName, 
                         string description, 
                         DateTime creationDate, 
                         TipoPerfil tipoPerfil) : base(roleName) 
        {
            Description = description;
            CreationDate = creationDate;
            TipoPerfil = tipoPerfil;
        }
    }
}
