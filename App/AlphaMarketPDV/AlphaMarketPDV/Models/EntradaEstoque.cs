using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public int UsuarioId { get; set; }

        [Display(Name = "Produtos")]
        public ICollection<ItemEntradaEstoque> ItensEntradaEstoque { get; set; } = new List<ItemEntradaEstoque>();

        public EntradaEstoque() 
        { 
        }

        public EntradaEstoque(int id, DateTime dataHora, double valorTotal, string observacao, 
                              DateTime dataHoraInformada, Fornecedor fornecedor, UsuarioApp usuario)
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
