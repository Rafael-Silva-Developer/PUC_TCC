using System.ComponentModel.DataAnnotations;

namespace AlphaMarketPDV.Models
{
    public class Contato
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Ramal")]
        public int Ramal { get; set; }

        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public Fornecedor Fornecedor { get; set; }

        [Display(Name = "Fornecedor")]
        public int FornecedorId { get; set; }

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
