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
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string NomeRepresentante { get; set; }
        public string EndComplemento { get; set; }
        public string EndNumero { get; set; }
        public string Site { get; set; }
        public TipoPessoa TipoEmpresa { get; set; }
        public string NumDocumento { get; set; }
        public string InscrEstadual { get; set; }
        public string InscrMunicipal { get; set; }
        public string Observacoes { get; set; }
        public bool Ativo { get; set; }
        public Endereco Endereco { get; set; }
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
