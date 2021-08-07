using System.Collections.Generic;

namespace AlphaMarketPDV.Models.ViewModels
{
    public class ProdutoFormViewModel
    {
        public Produto Produto { get; set; }
        public ICollection<Categoria> Categorias { get; set; }
        public ICollection<UnidadeMedida> UnidadesMedida { get; set; }

    }
}
