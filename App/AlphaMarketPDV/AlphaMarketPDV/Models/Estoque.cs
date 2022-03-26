using AlphaMarketPDV.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class Estoque
    {
        [Display(Name = "Id")]
        public int LojaId { get; set; }

        public Loja Loja { get; set; }

        [Display(Name = "Saldo")]
        [DisplayFormat(DataFormatString = "{0:F3}")]
        public double Saldo { get; set; }

        [Display(Name = "Status")]
        public StatusEstoque Status { get; set; }

        public Produto Produto { get; set; }

        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }

        public Estoque() 
        { 
        }

        public Estoque(Loja loja, Produto produto, double saldo, StatusEstoque status)
        {
            this.Loja = loja;
            this.Saldo = saldo;
            this.Status = status;
            this.Produto = produto;
        }

        public void AdicionarQtdProduto(double qtd) 
        {
            if (qtd > 0) 
            {
                Saldo += qtd;

                if (Produto.QuantMinima >= Saldo)
                {
                    Status = StatusEstoque.NORMAL;
                }
                else
                {
                    Status = StatusEstoque.BAIXO;
                }
            }
        }

        public void RemoverQtdProduto(double qtd) 
        {
            if (qtd > 0) 
            {
                Saldo -= qtd;

                if (Produto.QuantMinima >= Saldo)
                {
                    Status = StatusEstoque.NORMAL;
                }
                else 
                {
                    Status = StatusEstoque.BAIXO;
                }
            }
        }
    }
}
