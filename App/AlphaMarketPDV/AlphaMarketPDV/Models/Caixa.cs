using System;
using System.Collections.Generic;
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

        public UsuarioApp Usuario { get; set; }

        [Display(Name = "Usuário")]
        public string UsuarioId { get; set; }

        [StringLength(32)]
        public string IdentificadorRegistro { get; set; }
        public Caixa() 
        { 
        }

        public Caixa(int id, DateTime dataHora, double valor, 
            TipoCaixa tipoOperacao, UsuarioApp usuario, string identificadorRegistro)
        {
            Id = id;
            DataHora = dataHora;
            Valor = valor;
            TipoOperacao = tipoOperacao;
            Usuario = usuario;
            IdentificadorRegistro = identificadorRegistro;
        }

        public override string ToString()
        {
            return DataHora.ToString() +
                   Valor.ToString() +
                   TipoOperacao.ToString() +
                   UsuarioId.ToString();
        }




    }
}
