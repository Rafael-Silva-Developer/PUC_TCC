using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class SaidaEstoque
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Data/Hora Informada")]
        public DateTime DataHora { get; set; }

        [StringLength(150, MinimumLength = 4, ErrorMessage = "A justificativa dever ter entre 4 e 150 caracteres.")]
        [Required(ErrorMessage = "A justificativa é obrigatória!")]
        [Display(Name = "Justificativa")]
        public string Justificativa { get; set; }

        [Required(ErrorMessage = "A data/hora de saída é obrigatória!")]
        [Display(Name = "Data/Hora Saída")]
        public DateTime DataHoraInformada { get; set; }

        [Display(Name = "Valor Total")]
        public double ValorTotal { get; set; }

        public UsuarioApp Usuario { get; set; }

        [Display(Name = "Usuário")]
        public String UsuarioId { get; set; }

        [Display(Name = "Produtos")]
        public ICollection<ItemSaidaEstoque> ItensSaidaEstoque { get; set; } = new List<ItemSaidaEstoque>();

        [StringLength(32)]
        public string IdentificadorRegistro { get; set; }

        public SaidaEstoque() 
        { 
        }

        public SaidaEstoque(int id, DateTime dataHora, string justificativa, DateTime dataHoraInformada, 
                            double valorTotal, UsuarioApp usuario, string identificadorRegistro)
        {
            Id = id;
            DataHora = dataHora;
            Justificativa = justificativa;
            DataHoraInformada = dataHoraInformada;
            ValorTotal = valorTotal;
            Usuario = usuario;
            IdentificadorRegistro = identificadorRegistro;
        }

        public override string ToString()
        {
            return DataHora.ToString() +                  
                   Justificativa.ToString() +
                   DataHoraInformada.ToString() +
                   ValorTotal.ToString() +                   
                   UsuarioId.ToString();
        }
    }
}
