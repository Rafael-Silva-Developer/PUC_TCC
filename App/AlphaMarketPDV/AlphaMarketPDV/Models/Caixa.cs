using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Models.Enums;

namespace AlphaMarketPDV.Models
{
    public class Caixa
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public double Valor { get; set; }
        public TipoCaixa TipoOperacao { get; set; }
        public ICollection<CaixaPagamento> CaixaPagamentos { get; set; } = new List<CaixaPagamento>();
        public Usuario Usuario { get; set; }

        public Caixa() 
        { 
        }

        public Caixa(int id, DateTime dataHora, double valor, TipoCaixa tipoOperacao, Usuario usuario)
        {
            this.Id = id;
            this.DataHora = dataHora;
            this.Valor = valor;
            this.TipoOperacao = tipoOperacao;
            this.Usuario = usuario;
        }

        public void AdicionarItemCaixaPagamento(CaixaPagamento cp) 
        {
            CaixaPagamentos.Add(cp);
        }

        public void RemoverItemCaixaPagamento(CaixaPagamento cp) 
        {
            CaixaPagamentos.Remove(cp);
        }
    }
}
