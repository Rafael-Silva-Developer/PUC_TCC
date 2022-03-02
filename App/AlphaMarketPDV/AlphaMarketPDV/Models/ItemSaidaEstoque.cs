using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class ItemSaidaEstoque
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "NRSEQ")]
        public int NrSeq { get; set; }

        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double Valor { get; set; }

        [Display(Name = "Quantidade")]
        public double Qtd { get; set; }

        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double ValorTotal { get; set; }

        public Produto Produto { get; set; }

        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }

        public SaidaEstoque SaidaEstoque { get; set; }

        [Display(Name = "Saída")]
        public int SaidaEstoqueId { get; set; }

        public ItemSaidaEstoque() 
        { 
        }

        public ItemSaidaEstoque(int id, int nrSeq, double valor, double qtd, double valorTotal, 
                                Produto produto, int produtoId, SaidaEstoque saidaEstoque, int saidaEstoqueId)
        {
            Id = id;
            NrSeq = nrSeq;
            Valor = valor;
            Qtd = qtd;
            ValorTotal = valorTotal;
            Produto = produto;
            ProdutoId = produtoId;
            SaidaEstoque = saidaEstoque;
            SaidaEstoqueId = saidaEstoqueId;
        }
    }
}
