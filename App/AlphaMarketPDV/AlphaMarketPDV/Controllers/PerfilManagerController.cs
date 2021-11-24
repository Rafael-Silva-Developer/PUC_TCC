using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using AlphaMarketPDV.Services;
using AlphaMarketPDV.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace AlphaMarketPDV.Controllers
{
    [Authorize(Roles = "Supervisor")]
    public class PerfilManagerController : Controller
    {
        private readonly PerfilManagerService _perfilManagerService;
        private readonly UsuarioManagerService _usuarioManagerService;

        public PerfilManagerController(PerfilManagerService perfilManagerService,
                                       UsuarioManagerService usuarioManagerService)
        {
            _perfilManagerService = perfilManagerService;
            _usuarioManagerService = usuarioManagerService;
        }

        [HttpGet]
        public IActionResult Index() 
        {
            var  listaPerfils = _perfilManagerService.ListarTodosPerfis();
            return View(listaPerfils);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            if (Id == null) 
            {
                return RedirectToAction(nameof(Error), new { message = $"Id não informado para edição do perfil.", codigoErro = 404 });
            }

            var objPerfil = await _perfilManagerService.ListarPorIdAsync(Id);
            if (objPerfil == null) 
            {
                return RedirectToAction(nameof(Error), new { message = $"Perfil não foi localizado.", codigoErro = 404 });
            }

            var model = new EditarPerfilViewModel
            {
                Id = objPerfil.Id,
                NomePerfil = objPerfil.Name
            };

            var listaUsuarios = await _usuarioManagerService.ListarTodosUsuariosAsync();

            foreach (var usuario in listaUsuarios)
            {
                if (await _usuarioManagerService.UsuarioEstahNoPerfilAsync(usuario, objPerfil))
                {
                    model.ListaUsuarios.Add(usuario.Nome + " || " + usuario.UserName);
                }
            }

            model.ListaUsuarios.Sort();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditarPerfilViewModel model)
        {
            var perfil = await _perfilManagerService.ListarPorIdAsync(model.Id);
            if (perfil == null)
            {
                return RedirectToAction(nameof(Error), new { message = $"Perfil não encontrado.", codigoErro = 404 });
            }
            else
            {
                var result = await _perfilManagerService.AtualizarPerfilAsync(perfil);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarUsuariosNoPerfil(string roleId)
        {
            ViewBag.roleId = roleId;
            ViewBag.NomePerfil = "";

            var perfil = await _perfilManagerService.ListarPorIdAsync(roleId);
            if (perfil == null)
            {
                return RedirectToAction(nameof(Error), new { message = $"Perfil não encontrado.", codigoErro = 404 });
            }

            ViewBag.NomePerfil = perfil.Name;
            var model = new List<UsuarioPerfilViewModel>();
            var listaUsuarios = await _usuarioManagerService.ListarTodosUsuariosAsync();
            var perfilSupervisor = _perfilManagerService.ListarPerfilSupervisor();
            var perfilAtendente = _perfilManagerService.ListarPerfilAtendente();

            foreach (var user in listaUsuarios)
            {
                if (perfilSupervisor.Id == perfil.Id) 
                {
                    if (await _usuarioManagerService.UsuarioEstahNoPerfilAsync(user, perfilSupervisor))                    
                    {
                        var usuarioNoPerfil = new UsuarioPerfilViewModel
                        {
                            UsuarioId = user.Id,
                            UsuarioEmail = user.Nome +" || "+ user.UserName,
                            IsSelected = true
                        };
                        model.Add(usuarioNoPerfil);
                    }
                    else 
                    {
                        if (!await _usuarioManagerService.UsuarioEstahNoPerfilAsync(user, perfilAtendente)) 
                        {
                            var usuarioNoPerfil = new UsuarioPerfilViewModel
                            {
                                UsuarioId = user.Id,
                                UsuarioEmail = user.Nome + " || " + user.UserName,
                                IsSelected = false
                            };
                            model.Add(usuarioNoPerfil);
                        }
                    }
                }

                if (perfilAtendente.Id == perfil.Id)
                {
                    if (await _usuarioManagerService.UsuarioEstahNoPerfilAsync(user, perfilAtendente))
                    {
                        var usuarioNoPerfil = new UsuarioPerfilViewModel
                        {
                            UsuarioId = user.Id,
                            UsuarioEmail = user.Nome + " || " + user.UserName,
                            IsSelected = true
                        };
                        model.Add(usuarioNoPerfil);
                    }
                    else
                    {
                        if (!await _usuarioManagerService.UsuarioEstahNoPerfilAsync(user, perfilSupervisor))
                        {
                            var usuarioNoPerfil = new UsuarioPerfilViewModel
                            {
                                UsuarioId = user.Id,
                                UsuarioEmail = user.Nome + " || " + user.UserName,
                                IsSelected = false
                            };
                            model.Add(usuarioNoPerfil);
                        }
                    }
                }
            }
            return View(model.OrderBy(p => p.UsuarioEmail).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> EditarUsuariosNoPerfil(List<UsuarioPerfilViewModel> model, string roleId)
        {
            var perfil = await _perfilManagerService.ListarPorIdAsync(roleId);
            if (perfil == null)
            {
                return RedirectToAction(nameof(Error), new { message = $"Perfil não encontrado.", codigoErro = 404 });
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _usuarioManagerService.ListarUsuarioPorIdAsync(model[i].UsuarioId); 
                IdentityResult result = null;

                if (model[i].IsSelected && !(await _usuarioManagerService.UsuarioEstahNoPerfilAsync(user, perfil)))            
                {
                    result = await _usuarioManagerService.AdicionarUsuarioNoPerfilAsync(user, perfil);
                }
                else if (!model[i].IsSelected && await _usuarioManagerService.UsuarioEstahNoPerfilAsync(user, perfil))
                {
                    result = await _usuarioManagerService.RemoverUsuarioDoPerfilAsync(user, perfil);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("Edit", new { Id = roleId });
                }
            }
            return RedirectToAction("Edit", new { Id = roleId });
        }

        public IActionResult Error(string message, int codigoErro)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Codigo = codigoErro };
            return View(viewModel);
        }
    }
}
