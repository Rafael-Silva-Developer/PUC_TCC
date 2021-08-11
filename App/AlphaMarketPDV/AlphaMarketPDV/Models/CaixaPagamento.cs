using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class CaixaPagamento
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "NrSeq")]
        public int NrSeq { get; set; }

        [Display(Name = "Valor Pago")]
        public double ValorPago { get; set; }

        [Display(Name = "Valor Troco")]
        public double ValoTroco { get; set; }

        public Caixa Caixa { get; set; }

        [Display(Name = "Caixa")]
        public int CaixaId { get; set; }

        public FormaPagamento FormaPagamento { get; set; }

        [Display(Name = "Forma de Pagamento")]
        public int FormaPagamentoId { get; set; }

        public CaixaPagamento() 
        { 
        }

        public CaixaPagamento(int id, int nrSeq, double valorPago, double valoTroco, 
                              Caixa caixa, FormaPagamento formaPagamento)
        {
            this.Id = id;
            this.NrSeq = nrSeq;
            this.ValorPago = valorPago;
            this.ValoTroco = valoTroco;
            this.Caixa = caixa;
            this.FormaPagamento = formaPagamento;
        }
    }
}
