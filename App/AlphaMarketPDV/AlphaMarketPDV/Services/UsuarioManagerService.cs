using System;
using Microsoft.AspNetCore.Hosting;
using AlphaMarketPDV.Models;
using System.IO;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace AlphaMarketPDV.Services
{
    public class UsuarioManagerService
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly UserManager<UsuarioApp> _userManager;
        private readonly SignInManager<UsuarioApp> _signInManager;
        private readonly PerfilManagerService _perfilManagerService;

        public UsuarioManagerService(IHostingEnvironment appEnvironment,
                                     UserManager<UsuarioApp> userManager,
                                     SignInManager<UsuarioApp> signInManager,
                                     PerfilManagerService perfilManagerService)
        {
            _appEnvironment = appEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _perfilManagerService = perfilManagerService;
        }

        public async Task<List<UsuarioApp>> ListarTodosUsuariosAsync()
        {
            return await _userManager.Users.Cast<UsuarioApp>().ToListAsync();
        }

        public List<UsuarioApp> ListarTodosUsuarios()
        {
            return _userManager.Users.ToList();
        }

        public async Task<UsuarioApp> ListarUsuarioPorIdAsync(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }

        public async Task<List<System.Security.Claims.Claim>> ListarAtributosUsuariosAsync(UsuarioApp usuario)
        {
            return (List<System.Security.Claims.Claim>)await _userManager.GetClaimsAsync(usuario);
        }

        public async Task<List<string>> ListarPerfilsUsuarioAsync(UsuarioApp usuario)
        {
            return (List<string>)await _userManager.GetRolesAsync(usuario);
        }

        public async Task<bool> UsuarioEstahNoPerfilAsync(UsuarioApp usuario, PerfilApp perfil)
        {
            return await _userManager.IsInRoleAsync(usuario, perfil.Name);
        }

        public async Task<IdentityResult> AdicionarUsuarioNoPerfilAsync(UsuarioApp usuario, PerfilApp perfil)
        {
            return await _userManager.AddToRoleAsync(usuario, perfil.Name);
        }

        public async Task<bool> UsuarioCadastradoLocalmente(UsuarioApp usuario) 
        {
            var usuarioAux = await ListarUsuarioPorIdAsync(usuario.Id);
            return (usuarioAux.CadastroLocal);
        }

        public async Task<IdentityResult> RemoverUsuarioDoPerfilAsync(UsuarioApp usuario, PerfilApp perfil)
        {
            return await _userManager.RemoveFromRoleAsync(usuario, perfil.Name);
        }

        public async Task<SignInResult> AutenticarUsuarioSenhaAsync(string email, string senha, bool lembrar)
        {
            return await _signInManager.PasswordSignInAsync(email, senha, lembrar, lockoutOnFailure: false);
        }

        public async Task<IdentityResult> CriarUsuarioAsync(UsuarioApp usuario, string senha)
        {
            return await _userManager.CreateAsync(usuario, senha);
        }

        public async Task<IdentityResult> AtualizarUsuarioAsync(UsuarioApp usuario)
        {
            return await _userManager.UpdateAsync(usuario);
        }

        public async Task DesconectarUsuarioAsync()
        {
            await _signInManager.SignOutAsync();
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

        public async Task<PerfilApp> RetornarPerfilUsuarioAsync(UsuarioApp usuarioApp)
        {
            var perfilSupervisor = _perfilManagerService.ListarPerfilSupervisor();
            var perfilAtendente = _perfilManagerService.ListarPerfilAtendente();

            if (await UsuarioEstahNoPerfilAsync(usuarioApp, perfilSupervisor))
            {
                return perfilSupervisor;
            }
            else
            {
                if (await UsuarioEstahNoPerfilAsync(usuarioApp, perfilAtendente))
                {
                    return perfilAtendente;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<IList<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync() 
        {
            return (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string returnUrl) 
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync() 
        {
            return await _signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<SignInResult> ExternalLoginSignInAsync(string LoginProvider, string ProviderKey, bool isPersistent, bool bypassTwoFactor) 
        {
            return await _signInManager.ExternalLoginSignInAsync(LoginProvider, ProviderKey, isPersistent, bypassTwoFactor);
        }

        public async Task<UsuarioApp> ListarUsuarioEmailAsync(string email) 
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> AdicionarLoginAsync(UsuarioApp user, UserLoginInfo info)
        {
            return await _userManager.AddLoginAsync(user, info);
        }

        public async Task AutenticarLoginExternoAsync(UsuarioApp usuario, bool isPersistent) 
        {
            await _signInManager.SignInAsync(usuario, isPersistent);
        }

        public async Task<IdentityResult> CriarUsuarioSemSenhaAsync(UsuarioApp usuario)
        {
            return await _userManager.CreateAsync(usuario);
        }

        public async Task<string> RecuperarNomeUsuarioAsync(UsuarioApp usuario) 
        {
            return await _userManager.GetUserNameAsync(usuario);
        }

        public async Task<UsuarioApp> ListarUsuarioPorNomeAsync(string nome) 
        {
            return await _userManager.FindByNameAsync(nome);
        }

        public async Task<IdentityResult> AtualizarSenhaAsync(UsuarioApp usuario, string senha) 
        {
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(usuario);
            return await _userManager.ResetPasswordAsync(usuario, resetToken, senha);
        }
    }
}
