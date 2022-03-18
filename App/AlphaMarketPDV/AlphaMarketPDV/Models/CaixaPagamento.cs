using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class CaixaPagamento
    {
        [Display(Name = "#")]
        public int Id { get; set; }

        [Display(Name = "#")]
        public int NrSeq { get; set; }

        [Display(Name = "Valor Pago")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double ValorPago { get; set; }

        [Display(Name = "Valor Troco")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
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
            Id = id;
            NrSeq = nrSeq;
            ValorPago = valorPago;
            ValoTroco = valoTroco;
            Caixa = caixa;
            FormaPagamento = formaPagamento;
        }
    }
}
