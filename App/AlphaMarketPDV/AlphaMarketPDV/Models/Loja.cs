using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class Loja
    {
        [Display(Name = "#")]
        public int Id { get; set; }

        [StringLength(30, ErrorMessage = "A descrição da loja deve ter até 30 caracteres.")]
        [Required(ErrorMessage = "A descrição da loja é obrigatória!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [StringLength(20, ErrorMessage = "O complemento deve ter até 100 caracteres.")]
        [Display(Name = "Complemento")]
        public string EndComplemento { get; set; }

        [StringLength(8, ErrorMessage = "O número deve ter até 8 caracteres.")]
        [Display(Name = "Número")]
        public string EndNumero { get; set; }

        public Endereco Endereco { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório!")]
        [Display(Name = "Endereço")]
        public int EnderecoId { get; set; }

        [Display(Name = "Usuários")]
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public ICollection<Estoque> Estoques { get; set; } = new List<Estoque>();

        public Loja() 
        {
        }

        public Loja(int id, string descricao, string endComplemento, string endNumero, Endereco endereco)
        {
            this.Id = id;
            this.Descricao = descricao;
            this.EndComplemento = endComplemento;
            this.EndNumero = endNumero;
            this.Endereco = endereco;
        }

        public void AdicionarUsuario(Usuario u)
        {
            Usuarios.Add(u);
        }

        public void RemoverUsuario(Usuario u)
        {
            Usuarios.Remove(u);
        }

        public void AdicionarEstoque(Estoque e)
        {
            Estoques.Add(e);
        }

        public void RemoverEstoque(Estoque e)
        {
            Estoques.Remove(e);
        }
    }
}
