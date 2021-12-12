using System.Collections.Generic;

namespace AlphaMarketPDV.Models.ViewModels.FornecedorView
{
    public class FornecedorViewModel
    {
        public Fornecedor Fornecedor { get; set; }

        public Contato Contato { get; set; }

        public ICollection<Contato> ListaContatos { get; set; }

        public FornecedorViewModel() 
        {
            ListaContatos = new List<Contato>();
        }
    }
}
