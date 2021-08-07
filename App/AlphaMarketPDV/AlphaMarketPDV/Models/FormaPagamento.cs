using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class FormaPagamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool GeraTroco { get; set; }
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
