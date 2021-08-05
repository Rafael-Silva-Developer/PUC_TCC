using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaMarketPDV.Models
{
    public class EntradaEstoque
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public double ValorTotal { get; set; }
        public string Observacao { get; set; }
        public DateTime DataHoraInformada { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<ItemEntradaEstoque> ItensEntradaEstoque { get; set; } = new List<ItemEntradaEstoque>();

        public EntradaEstoque() 
        { 
        }

        public EntradaEstoque(int id, DateTime dataHora, double valorTotal, string observacao, 
                              DateTime dataHoraInformada, Fornecedor fornecedor, Usuario usuario)
        {
            this.Id = id;
            this.DataHora = dataHora;
            this.ValorTotal = valorTotal;
            this.Observacao = observacao;
            this.DataHoraInformada = dataHoraInformada;
            this.Fornecedor = fornecedor;
            this.Usuario = usuario;
        }

        public void AdicionarItemEntradaEstoque(ItemEntradaEstoque iee) 
        {
            ItensEntradaEstoque.Add(iee);
        }

        public void RemoverItemEntradaEstoque(ItemEntradaEstoque iee) 
        {
            ItensEntradaEstoque.Remove(iee);
        }
    }
}
