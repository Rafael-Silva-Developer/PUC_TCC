using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class SaidaEstoque
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Data/Hora Saída")]
        public DateTime DataHora { get; set; }

        [StringLength(150, MinimumLength = 4, ErrorMessage = "A justificativa dever ter entre 4 e 150 caracteres.")]
        [Required(ErrorMessage = "A justificativa é obrigatória!")]
        [Display(Name = "Justificativa")]
        public string Justificativa { get; set; }

        [Required(ErrorMessage = "A data/hora de saída é obrigatória!")]
        [Display(Name = "Data/Hora Informada")]
        public DateTime DataHoraInformada { get; set; }

        [Display(Name = "Valor Total")]
        public double ValorTotal { get; set; }

        public UsuarioApp Usuario { get; set; }

        [Display(Name = "Usuário")]
        public int UsuarioId { get; set; }

        [Display(Name = "Produtos")]
        public ICollection<ItemSaidaEstoque> ItensSaidaEstoque { get; set; } = new List<ItemSaidaEstoque>();

        public SaidaEstoque() 
        { 
        }

        public SaidaEstoque(int id, DateTime dataHora, string justificativa, DateTime dataHoraInformada, 
                            double valorTotal, UsuarioApp usuario)
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
