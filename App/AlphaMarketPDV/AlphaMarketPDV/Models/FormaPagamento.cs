using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class FormaPagamento
    {
        [Display(Name = "#")]
        public int Id { get; set; }

        [StringLength(30, ErrorMessage = "A descrição da forma de pagamento dever ter no máximo 30 caracteres.")]
        [Required(ErrorMessage = "A descrição da forma de pagamento é obrigatória!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Gera Troco")]
        public bool GeraTroco { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
      
        public FormaPagamento() 
        { 
        }

        public FormaPagamento(int id, string descricao, bool geraTroco, bool ativo)
        {
            this.Id = id;
            this.Descricao = descricao;
            this.GeraTroco = geraTroco;
            this.Ativo = ativo;
        }
    }
}
