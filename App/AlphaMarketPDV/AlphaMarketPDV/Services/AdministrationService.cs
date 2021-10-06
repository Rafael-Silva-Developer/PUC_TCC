using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AlphaMarketPDV.Models;

namespace AlphaMarketPDV.Services
{
    public class AdministrationService
    {
        private readonly RoleManager<PerfilApp> _roleManager;
        private readonly UserManager<UsuarioApp> _userManager;

        public AdministrationService(RoleManager<PerfilApp> roleManager, UserManager<UsuarioApp> userManager) 
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public List<PerfilApp> ListarTodosPerfis()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<PerfilApp> ListarPorIdAsync(string id)
        {
            return await _roleManager.FindByNameAsync(id);         
        }
    }
}
