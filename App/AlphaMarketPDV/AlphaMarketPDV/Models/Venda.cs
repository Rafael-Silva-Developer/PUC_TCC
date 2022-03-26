using System;
using System.Collections.Generic;
using AlphaMarketPDV.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class Venda
    {
        [Display(Name = "#")]
        public int Id { get; private set; }

        [Display(Name = "Data/Hora Venda")]
        public DateTime DataHora { get; set; }

        [Display(Name = "Data Venda")]
        public DateTime DataVenda { get; set; }

        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double ValorTotal { get; set; }

        [Display(Name = "Valor Desconto")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double ValorDesconto { get; set; }

        [Display(Name = "Valor a Pagar")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double TotalPagar { get; set; }
        
        [Display(Name = "Status")]
        public StatusVenda Status { get; set; }

        public Caixa Caixa { get; set; }

        [Display(Name = "Caixa")]
        public int CaixaId { get; set; }

        [StringLength(32)]
        public string IdentificadorRegistro { get; set; }

        [Display(Name = "Itens")]
        public ICollection<ItemVenda> ItensVenda { get; set; } = new List<ItemVenda>();

        public Venda() 
        { 
        }

        public Venda(int id, DateTime dataHora, DateTime dataVenda, double valorTotal, double valorDesconto, double totalPagar,
                     StatusVenda status, Caixa caixa, string identificadorRegistro)
        {
            Id = id;
            DataHora = dataHora;
            DataVenda = dataVenda;
            ValorTotal = valorTotal;
            ValorDesconto = valorDesconto;
            TotalPagar = totalPagar;
            Status = status;
            Caixa = caixa;
            IdentificadorRegistro = identificadorRegistro;
        }

        public override string ToString()
        {
            return DataHora.ToString() +
                   ValorTotal.ToString() +
                   ValorDesconto.ToString() +
                   TotalPagar.ToString() +
                   Status.ToString() +
                   CaixaId.ToString();
        }


    }
}
