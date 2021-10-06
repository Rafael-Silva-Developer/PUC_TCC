using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Models.Infra;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using AlphaMarketPDV.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlphaMarketPDV.Models.ViewModels;
using System.Diagnostics;

namespace AlphaMarketPDV.Controllers
{
    [Authorize]
    public class InfraController : Controller
    {
        private readonly UserManager<UsuarioApp> _userManager;
        private readonly SignInManager<UsuarioApp> _signInManager;
        private readonly ILogger _logger;
        private readonly LojaService _lojaService;
        private readonly InfraService _infraService;

        public InfraController(UserManager<UsuarioApp> userManager,
                               SignInManager<UsuarioApp> signInManager,
                               ILogger<InfraController> logger,
                               LojaService lojaService,
                               InfraService infraService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _lojaService = lojaService;
            _infraService = infraService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listaUsuarios = await _infraService.ListarTodosUsuariosAsync();
            return View(listaUsuarios);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Acessar(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Acessar(AcessarViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, model.LembrarDeMim, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuário Autenticado.");
                    return RedirectToLocal(returnUrl);
                }
            }

            ModelState.AddModelError(string.Empty, "Falha na tentativa de login.");
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrarNovoUsuario(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var lojas = await _lojaService.ListarTodosAsync();
            var viewModel = new RegistrarNovoUsuarioViewModel { ListaLojas = lojas };
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarNovoUsuario(RegistrarNovoUsuarioViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                var lojas = await _lojaService.ListarTodosAsync();
                var viewModel = new RegistrarNovoUsuarioViewModel { Email = model.Email, ListaLojas = lojas };
                return View(viewModel);
            }
            else 
            {
                if (model.Usuario.FotoUsuarioLoad != null)
                {
                    string nomeFotoProduto = _infraService.UploadImagemUsuario(model.Usuario);                
                    model.Usuario.FotoUsuario = nomeFotoProduto;
                }

                var user = new UsuarioApp
                {
                    Nome = model.Usuario.Nome,
                    FotoUsuario = model.Usuario.FotoUsuario,
                    Ativo = model.Usuario.Ativo,
                    Tipo = model.Usuario.Tipo,
                    LojaId = model.Usuario.LojaId,
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuário criou uma nova conta com senha.");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    AddErrors(result);
                    var lojas = await _lojaService.ListarTodosAsync();
                    var viewModel = new RegistrarNovoUsuarioViewModel { Email = model.Email, ListaLojas = lojas };
                    return View(viewModel);
                }
            }         
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

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

        [HttpGet]
        public async Task<IActionResult> Sair()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Usuário realizou logout.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(viewModel);
        }

    }
}
