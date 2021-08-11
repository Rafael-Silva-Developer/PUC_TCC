using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class Fornecedor
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "A razão social dever ter até 100 caracteres.")]
        [Required(ErrorMessage = "A razão social é obrigatória!")]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [StringLength(100, ErrorMessage = "O nome fantasia devem ter até 100 caracteres.")]
        [Required(ErrorMessage = "O nome fantasia é obrigatório!")]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [StringLength(60, ErrorMessage = "O nome do representante devem ter até 60 caracteres.")]
        [Display(Name = "Nome do Representante")]
        public string NomeRepresentante { get; set; }

        [StringLength(20, ErrorMessage = "O complemento deve ter até 100 caracteres.")]
        [Display(Name = "Complemento")]
        public string EndComplemento { get; set; }

        [StringLength(8, ErrorMessage = "O número deve ter até 8 caracteres.")]
        [Display(Name = "Número")]
        public string EndNumero { get; set; }

        [StringLength(60, ErrorMessage = "O site deve ter até 60 caracteres.")]
        [Display(Name = "Site")]
        public string Site { get; set; }

        [Required(ErrorMessage = "O tipo de fornecedor é obrigatório!")]
        [Display(Name = "Tipo de Fornecedor")]
        public TipoPessoa TipoEmpresa { get; set; }

        [StringLength(60, ErrorMessage = "O número do documento deve ter até 60 caracteres.")]
        [Required(ErrorMessage = "O número do documento é obrigatório!")]
        [Display(Name = "N.º Documento")]
        public string NumDocumento { get; set; }

        [StringLength(30, ErrorMessage = "A inscrição estadual deve ter até 30 caracteres.")]
        [Display(Name = "Inscrição Estadual")]
        public string InscrEstadual { get; set; }

        [StringLength(30, ErrorMessage = "A inscrição municipal deve ter até 30 caracteres.")]
        [Display(Name = "Inscrição Municipal")]
        public string InscrMunicipal { get; set; }

        [StringLength(100, ErrorMessage = "As observações devem ter até 100 caracteres.")]
        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        public Endereco Endereco { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório!")]
        [Display(Name = "Endereço")]
        public int EnderecoId { get; set; }

        [Display(Name = "Contatos")]
        public ICollection<Contato> Contatos { get; set; } = new List<Contato>();

        public Fornecedor() 
        { 
        }

        public Fornecedor(int id, string razaoSocial, string nomeFantasia, string nomeRepresentante, 
                          string endComplemento, string endNumero, string site, TipoPessoa tipoEmpresa, 
                          string numDocumento, string inscrEstadual, string inscrMunicipal, string observacoes, 
                          bool ativo, Endereco endereco)
        {
            this.Id = id;
            this.RazaoSocial = razaoSocial;
            this.NomeFantasia = nomeFantasia;
            this.NomeRepresentante = nomeRepresentante;
            this.EndComplemento = endComplemento;
            this.EndNumero = endNumero;
            this.Site = site;
            this.TipoEmpresa = tipoEmpresa;
            this.NumDocumento = numDocumento;
            this.InscrEstadual = inscrEstadual;
            this.InscrMunicipal = inscrMunicipal;
            this.Observacoes = observacoes;
            this.Ativo = ativo;
            this.Endereco = endereco;
        }

        public void AdicionarContato(Contato c) 
        {
            Contatos.Add(c);
        }

        public void RemoverContato(Contato c) 
        {
            Contatos.Remove(c);
        }
    }
}
