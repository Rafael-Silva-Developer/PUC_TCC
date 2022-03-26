using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class EntradaEstoque
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Data/Hora Entrada")]
        public DateTime DataHora { get; set; }

        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double ValorTotal { get; set; }

        [StringLength(100, ErrorMessage = "A observação dever ter no máximo 100 caracteres.")]
        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Required(ErrorMessage = "A data/hora de entrada é obrigatória!")]
        [Display(Name = "Data/Hora Informado")]
        public DateTime DataHoraInformada { get; set; }

        public Fornecedor Fornecedor { get; set; }

        [Display(Name = "Fornecedor")]
        public int FornecedorId { get; set; }

        public UsuarioApp Usuario { get; set; }

        [Display(Name = "Usuário")]
        public String UsuarioId { get; set; }

        [Display(Name = "Produtos")]
        public ICollection<ItemEntradaEstoque> ItensEntradaEstoque { get; set; } = new List<ItemEntradaEstoque>();

        [StringLength(32)]
        public string IdentificadorRegistro { get; set; }

        public EntradaEstoque() 
        { 
        }

        public EntradaEstoque(int id, DateTime dataHora, double valorTotal, string observacao, 
                              DateTime dataHoraInformada, Fornecedor fornecedor, UsuarioApp usuario,
                              string identificadorRegistro)
        {
            Id = id;
            DataHora = dataHora;
            ValorTotal = valorTotal;
            Observacao = observacao;
            DataHoraInformada = dataHoraInformada;
            Fornecedor = fornecedor;
            Usuario = usuario;
            IdentificadorRegistro = identificadorRegistro;
        }

        public override string ToString()
        {
            return DataHora.ToString() +
                   ValorTotal.ToString() +
                   Observacao.ToString() +
                   DataHoraInformada.ToString() +
                   FornecedorId.ToString() +
                   UsuarioId.ToString();                   
        }
    }
}
