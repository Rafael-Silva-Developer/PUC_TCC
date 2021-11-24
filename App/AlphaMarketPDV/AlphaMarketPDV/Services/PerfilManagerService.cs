using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Models.Enums;
using AlphaMarketPDV.Data;
using Microsoft.EntityFrameworkCore.Internal;

namespace AlphaMarketPDV.Services
{
    public class PerfilManagerService
    {
        private readonly RoleManager<PerfilApp> _roleManager;
        private readonly AlphaMarketPDVContext _context;

        public PerfilManagerService(RoleManager<PerfilApp> roleManager,
                                    AlphaMarketPDVContext context)
        {
            _roleManager = roleManager;
            _context = context;
    }

        public List<PerfilApp> ListarTodosPerfis()
        {
            return _roleManager.Roles.Cast<PerfilApp>().ToList();
        }

        public async Task<PerfilApp> ListarPorIdAsync(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> AtualizarPerfilAsync(PerfilApp perfil)
        {
            return await _roleManager.UpdateAsync(perfil);
        }

        public PerfilApp ListarPerfilSupervisor()
        {
            return _roleManager.Roles.Where(ps => ps.TipoPerfil == TipoPerfil.SUPERVISOR).First();
        }

        public PerfilApp ListarPerfilAtendente()
        {
            return _roleManager.Roles.Where(pa => pa.TipoPerfil == TipoPerfil.ATENDENTE).First();
        }

        public int QuantidadeUsuariosPerfilAsync(PerfilApp perfil)
        {           
            var qtd = _context.UserRoles.ToList();
            return qtd.Count(ur => ur.RoleId == perfil.Id);    
        }

       
            //para alterar senha... ChangePasswordAsync(TUser, String, String);

        
    }
}
