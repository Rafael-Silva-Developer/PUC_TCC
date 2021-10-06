using System;
using Microsoft.AspNetCore.Hosting;
using AlphaMarketPDV.Models;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AlphaMarketPDV.Services
{
    public class InfraService
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly UserManager<UsuarioApp> _userManager;
        private readonly SignInManager<UsuarioApp> _signInManager;
        private readonly ILogger _logger;

        public InfraService(IHostingEnvironment appEnvironment,
                            UserManager<UsuarioApp> userManager,
                            SignInManager<UsuarioApp> signInManager,
                            ILogger<InfraService> logger) 
        {
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<List<UsuarioApp>> ListarTodosUsuariosAsync()
        {
            return await _userManager.Users.Cast<UsuarioApp>().ToListAsync();
        }

        public async Task<bool> UsuarioEstahNoPerfil(UsuarioApp usuario, PerfilApp perfil) 
        {
            return await _userManager.IsInRoleAsync(usuario, perfil.Name);
        }





        public string UploadImagemUsuario(UsuarioApp usuarioApp)
        {
            string pastaFotos = Path.Combine(_appEnvironment.WebRootPath, "images\\usuarios");
            string nomeUnicoArquivo = Guid.NewGuid().ToString() + "." + Path.GetExtension(usuarioApp.FotoUsuarioLoad.FileName);
            string caminhoArquivo = Path.Combine(pastaFotos, nomeUnicoArquivo);
            using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                usuarioApp.FotoUsuarioLoad.CopyTo(fileStream);
            }
            return nomeUnicoArquivo;
        }

        public void ExcluirImagemUsuario(UsuarioApp usuarioApp)
        {
            if ((usuarioApp.FotoUsuario != null) && (usuarioApp.FotoUsuario != ""))
            {
                string pastaFotos = Path.Combine(_appEnvironment.WebRootPath, "images\\usuarios");
                string nomeArquivo = usuarioApp.FotoUsuario;
                string caminhoArquivo = Path.Combine(pastaFotos, nomeArquivo);
                File.Delete(caminhoArquivo);
            }
        }
    }
}
