using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaMarketPDV.Models
{
    public class SaidaEstoque
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Justificativa { get; set; }
        public DateTime DataHoraInformada { get; set; }
        public double ValorTotal { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<ItemSaidaEstoque> ItensSaidaEstoque { get; set; } = new List<ItemSaidaEstoque>();

        public SaidaEstoque() 
        { 
        }

        public SaidaEstoque(int id, DateTime dataHora, string justificativa, DateTime dataHoraInformada, 
                            double valorTotal, Usuario usuario)
        {
            this.Id = id;
            this.DataHora = dataHora;
            this.Justificativa = justificativa;
            this.DataHoraInformada = dataHoraInformada;
            this.ValorTotal = valorTotal;
            this.Usuario = usuario;
        }

        public void AdicionarItemSaidaEstoque(ItemSaidaEstoque ise) 
        {
            ItensSaidaEstoque.Add(ise);
        }

        public void RemoverItemSaidaEstoque(ItemSaidaEstoque ise) 
        {
            ItensSaidaEstoque.Remove(ise);
        }
    }
}
