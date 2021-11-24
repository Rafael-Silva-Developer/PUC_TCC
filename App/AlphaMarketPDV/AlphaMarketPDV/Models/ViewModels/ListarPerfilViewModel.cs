using System.Collections.Generic;

namespace AlphaMarketPDV.Models.ViewModels
{
    public class ListarPerfilViewModel
    {
        public PerfilApp PerfilApp;

        public ICollection<UsuarioApp> ListaUsuarios { get; set; } = new List<UsuarioApp>();

        public ListarPerfilViewModel() 
        {
        }
    }
}
