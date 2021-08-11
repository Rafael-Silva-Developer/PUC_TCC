using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class Endereco
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [Display(Name = "Lougradouro")]
        public string Lougradouro { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "UF")]
        public string Uf { get; set; }

        [Display(Name = "Fornecedores")]
        public ICollection<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();

        public Endereco() 
        { 
        }

        public Endereco(int id, string cep, string lougradouro, string bairro, string cidade, string uf)
        {
            this.Id = id;
            this.Cep = cep;
            this.Lougradouro = lougradouro;
            this.Bairro = bairro;
            this.Cidade = cidade;
            this.Uf = uf;
        }

        public void AdicionarFornecedor(Fornecedor f) 
        {
            Fornecedores.Add(f);
        }

        public void RemoverFornecedor(Fornecedor f) 
        {
            Fornecedores.Remove(f);
        }
    }
}
