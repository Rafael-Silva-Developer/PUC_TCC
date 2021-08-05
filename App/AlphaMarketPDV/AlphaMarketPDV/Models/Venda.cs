using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Models.Enums;

namespace AlphaMarketPDV.Models
{
    public class Venda
    {
        public int Id { get; private set; }
        public DateTime DataHora { get; private set; }
        public double ValorTotal { get; private set; }
        public double ValorDesconto { get; private set; }
        public StatusVenda Status { get; set; }
        public Caixa Caixa { get; set; }
        public ICollection<ItemVenda> ItensVenda { get; set; } = new List<ItemVenda>();

        public Venda() 
        { 
        }

        public Venda(int id, DateTime dataHora, double valorTotal, double valorDesconto, 
                     StatusVenda status, Caixa caixa)
        {
            this.Id = id;
            this.DataHora = dataHora;
            this.ValorTotal = valorTotal;
            this.ValorDesconto = valorDesconto;
            this.Status = status;
            this.Caixa = caixa;
        }

        public void AdicionarItemVenda(ItemVenda iv) 
        {
            ItensVenda.Add(iv);
        }

        public void RemoverItemVenda(ItemVenda iv) 
        {
            ItensVenda.Remove(iv);
        }

        public void TotalVenda() 
        {
            ValorTotal = ItensVenda.Where(iv => iv.Cancelado != false).Sum(iv => iv.ValorItem);
        }
    }
}
