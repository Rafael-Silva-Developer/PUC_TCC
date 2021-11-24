using System.Collections.Generic;

namespace AlphaMarketPDV.Models.ViewModels
{
    public class FornecedorViewModel
    {
        public Fornecedor Fornecedor { get; set; }

        public ICollection<Contato> Contatos { get; set; }

        public Endereco Endereco { get; set; }
    }
}
