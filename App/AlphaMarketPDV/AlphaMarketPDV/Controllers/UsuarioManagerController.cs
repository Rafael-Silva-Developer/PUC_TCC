using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using AlphaMarketPDV.Services;
using AlphaMarketPDV.Models.ViewModels;
using AlphaMarketPDV.Models;
using System.Linq;
using System.Security.Claims;

namespace AlphaMarketPDV.Controllers
{
    [Authorize(Roles = "Supervisor")]
    public class UsuarioManagerController : Controller
    {
        private readonly LojaService _lojaService;
        private readonly UsuarioManagerService _usuarioManagerService;
        private readonly PerfilManagerService _perfilManagerService;

        public UsuarioManagerController(LojaService lojaService, UsuarioManagerService usuarioManagerService,
                                        PerfilManagerService perfilManagerService)
        {
            _lojaService = lojaService;
            _usuarioManagerService = usuarioManagerService;
            _perfilManagerService = perfilManagerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listaUsuarios = await _usuarioManagerService.GetUsuariosAsync();        
            return View(listaUsuarios);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var usuario = await _usuarioManagerService.GetUsuarioPorIdAsync(id);

            if (usuario == null)
            {
                return RedirectToAction(nameof(Error), new { message = $"Usuário não encontrado para visualização.", codigoErro = 404 });
            }
            else
            {
                var loja = await _lojaService.GetLojaPorIdAsync(usuario.LojaId);
                var perfilSupervisor = _perfilManagerService.ListarPerfilSupervisor();
                var perfilAtendente = _perfilManagerService.ListarPerfilAtendente();
                var perfilUsuario = await _usuarioManagerService.RetornarPerfilUsuarioAsync(usuario);

                var model = new VisualizarUsuarioViewModel
                {
                    Usuario = usuario,
                    LojaUsuario = loja,
                    Perfil = perfilUsuario
                };

                return View(model);
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AlterarSenha() 
        {
            string currentUser = User.Identity.Name;
            var usuario = await _usuarioManagerService.ListarUsuarioPorNomeAsync(currentUser);

            if (await _usuarioManagerService.UsuarioCadastradoLocalmente(usuario))
            {
                var model = new AlterarSenhaViewModel { Id = usuario.Id };
                return View(model);
            }
            else 
            {
                return RedirectToAction(nameof(Error), new { message = $"Usuário autenticado externamente. Realize a alteração da senha no seu provedor de login!", codigoErro = 500 });
            }                 
        }

        [HttpPost]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaViewModel model)
        {
            var usuario = await _usuarioManagerService.GetUsuarioPorIdAsync(model.Id);
            if (usuario == null)
            {
                return RedirectToAction(nameof(Error), new { message = $"Usuário não encontrado para alteração da senha.", codigoErro = 404 });
            }
            else 
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else 
                {
                    var appUser = await _usuarioManagerService.GetUsuarioPorIdAsync(usuario.Id);
                    var result = await _usuarioManagerService.AtualizarSenhaAsync(appUser, model.NovaSenha);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    else 
                    {
                        return RedirectToAction(nameof(Error), new { message = $"Erro ao tentar alterar senha.", codigoErro = 500 });
                    }
                }
            }           
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var usuario = await _usuarioManagerService.GetUsuarioPorIdAsync(id);

            if (usuario == null)
            {
                return RedirectToAction(nameof(Error), new { message = $"Usuário não encontrado para edição.", codigoErro = 404 });
            }
            else
            {
                var lojas = await _lojaService.ListarTodosAsync();

                var model = new EditarUsuarioViewModel
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    UserName = usuario.UserName,
                    ListaLojas = lojas,
                    Usuario = usuario
                };

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditarUsuarioViewModel model)
        {
            var usuario = await _usuarioManagerService.GetUsuarioPorIdAsync(model.Id);

            if (usuario == null)
            {
                return RedirectToAction(nameof(Error), new { message = $"Usuário não encontrado para edição.", codigoErro = 404 });
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    var lojas = await _lojaService.ListarTodosAsync();
                    var modelAux = new EditarUsuarioViewModel
                    {
                        Id = model.Usuario.Id,
                        Email = model.Usuario.Email,
                        UserName = model.Usuario.Email,
                        ListaLojas = lojas,
                        Usuario = model.Usuario
                    };

                    return View(modelAux);
                }
                else
                {
                    usuario.Email = model.Email;
                    usuario.UserName = model.Email;
                    usuario.Nome = model.Usuario.Nome;
                    usuario.Ativo = model.Usuario.Ativo;

                    var result = await _usuarioManagerService.AtualizarUsuarioAsync(usuario);
                    if (result.Succeeded)
                    {
                        if (model.Usuario.FotoUsuarioLoad != null)
                        {
                            _usuarioManagerService.ExcluirImagemUsuario(model.Usuario);
                            string novaFotoUsuario = _usuarioManagerService.UploadImagemUsuario(model.Usuario);
                            usuario.FotoUsuario = novaFotoUsuario;
                            await _usuarioManagerService.AtualizarUsuarioAsync(usuario);
                        }
                        return RedirectToAction(nameof(Index));
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Acessar(string returnUrl = null)
        {
            AcessarViewModel model = new AcessarViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = await _usuarioManagerService.GetExternalAuthenticationSchemesAsync()
            };          
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "UsuarioManager",
                new { ReturnUrl = returnUrl });
            var properties = _usuarioManagerService.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            AcessarViewModel model = new AcessarViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = await _usuarioManagerService.GetExternalAuthenticationSchemesAsync()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Login", model);
            }

            var info = await _usuarioManagerService.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");
                return View("Login", model);
            }

            var signInResult = await _usuarioManagerService.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    var usuario = await _usuarioManagerService.ListarUsuarioEmailAsync(email);

                    if (usuario == null)
                    {
                        usuario = new UsuarioApp
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Nome = info.Principal.FindFirstValue(ClaimTypes.Name),
                            Ativo = true,
                            CadastroLocal = false,
                            LojaId = 1
                        };

                        await _usuarioManagerService.CriarUsuarioSemSenhaAsync(usuario);
                    }

                    await _usuarioManagerService.AdicionarLoginAsync(usuario, info);
                    await _usuarioManagerService.AutenticarLoginExternoAsync(usuario, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on @com.br";

                return View("Error");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Acessar(AcessarViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _usuarioManagerService.AutenticarUsuarioSenhaAsync(model.Email, model.Senha, model.LembrarDeMim);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
            }

            AcessarViewModel modelAux = new AcessarViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = await _usuarioManagerService.GetExternalAuthenticationSchemesAsync()
            };

            ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos!");
            return View(modelAux);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var lojas = await _lojaService.ListarTodosAsync();
            var viewModel = new NovoUsuarioViewModel { ListaLojas = lojas };
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NovoUsuarioViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                var lojas = await _lojaService.ListarTodosAsync();
                var viewModel = new NovoUsuarioViewModel { Email = model.Email, ListaLojas = lojas };
                return View(viewModel);
            }
            else
            {
                var user = new UsuarioApp
                {
                    Nome = model.Usuario.Nome,
                    FotoUsuario = model.Usuario.FotoUsuario,
                    Ativo = model.Usuario.Ativo,
                    CadastroLocal = true,
                    LojaId = model.Usuario.LojaId,
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _usuarioManagerService.CriarUsuarioAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.Usuario.FotoUsuarioLoad != null)
                    {
                        string novaFotoUsuario = _usuarioManagerService.UploadImagemUsuario(model.Usuario);
                        model.Usuario.FotoUsuario = novaFotoUsuario;
                    }

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    AddErrors(result);
                    var lojas = await _lojaService.ListarTodosAsync();
                    var viewModel = new NovoUsuarioViewModel { Email = model.Email, ListaLojas = lojas };
                    return View(viewModel);
                }
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logoff()
        {
            await _usuarioManagerService.DesconectarUsuarioAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [AllowAnonymous]
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        [AllowAnonymous]
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [AllowAnonymous]
        public IActionResult Error(string message, int codigoErro)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Codigo = codigoErro };
            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AcessoNegado()
        {
            return View();
        }
    }
}
