using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Lougradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
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
