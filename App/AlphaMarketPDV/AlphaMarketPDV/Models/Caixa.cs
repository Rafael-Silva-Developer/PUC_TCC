using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class Caixa
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data de operação é obrigatória!")]
        [Display(Name = "Data/Hora Operação")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "O valor da operação é obrigatório!")]
        [Display(Name = "Valor")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "O tipo de operação é obrigatório!")]
        [Display(Name = "Tipo de Operação")]
        public TipoCaixa TipoOperacao { get; set; }

        [Display(Name = "Detalhe Pagamento")]
        public ICollection<CaixaPagamento> CaixaPagamentos { get; set; } = new List<CaixaPagamento>();

        public Usuario Usuario { get; set; }

        [Display(Name = "Usuário")]
        public int UsuarioId { get; set; }

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
