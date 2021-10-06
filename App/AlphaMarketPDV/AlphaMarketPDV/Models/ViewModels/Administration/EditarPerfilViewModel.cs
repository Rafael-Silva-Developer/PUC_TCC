using System.Collections.Generic;

namespace AlphaMarketPDV.Models.ViewModels.Administration
{
    public class EditarPerfilViewModel
    {
        public string Id { get; set; }

        public string NomePerfil { get; set; }

        public List<string> ListaUsuarios { get; set; }

        public EditarPerfilViewModel() 
        {
            ListaUsuarios = new List<string>();
        }

    }
}
