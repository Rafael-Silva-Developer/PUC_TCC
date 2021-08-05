using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaMarketPDV.Models
{
    public class Contato
    {
        public int Id { get; set; }
        public string Telefone { get; set; }
        public int Ramal { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public Contato() 
        { 
        }

        public Contato(int id, string telefone, int ramal, string celular, string email, Fornecedor fornecedor)
        {
            this.Id = id;
            this.Telefone = telefone;
            this.Ramal = ramal;
            this.Celular = celular;
            this.Email = email;
            this.Fornecedor = fornecedor;
        }
    }
}
