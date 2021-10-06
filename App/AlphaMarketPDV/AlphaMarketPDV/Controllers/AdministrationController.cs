using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Models.ViewModels.Administration;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Models.Enums;
using AlphaMarketPDV.Services.Exceptions;
using AlphaMarketPDV.Services;
using System.Diagnostics;
using AlphaMarketPDV.Models.ViewModels;

namespace AlphaMarketPDV.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<PerfilApp> _roleManager;
        private readonly UserManager<UsuarioApp> _userManager;
        private readonly AdministrationService _administrationService;
        private readonly InfraService _infraService;

        public AdministrationController(RoleManager<PerfilApp> roleManager,
                                        UserManager<UsuarioApp> userManager,
                                        AdministrationService administrationService,
                                        InfraService infraService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _administrationService = administrationService;
            _infraService = infraService;
        }

        [HttpGet]
        public IActionResult Index() 
        {
            var listaPerfils = _administrationService.ListarTodosPerfis();
            return View(listaPerfils);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string IdPerfil)
        {
            if (IdPerfil == null) 
            {
                return RedirectToAction(nameof(Error), new { message = $"Id não informado para edição do perfil." });
            }

            var objPerfil = await _administrationService.ListarPorIdAsync(IdPerfil);
            if (objPerfil == null) 
            {
                return RedirectToAction(nameof(Error), new { message = $"O Perfil com id: {IdPerfil} não foi localizado." });
            }

            var model = new EditarPerfilViewModel
            {
                Id = objPerfil.Id,
                NomePerfil = objPerfil.Name
            };

            var listaUsuarios = await _infraService.ListarTodosUsuariosAsync();

            foreach (var usuario in listaUsuarios)
            {
                if (await _infraService.UsuarioEstahNoPerfil(usuario, objPerfil))
                {
                    model.ListaUsuarios.Add(usuario.Nome + " / " + usuario.UserName);
                }
            }

            model.ListaUsuarios.Sort();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditarPerfil(EditarPerfilViewModel model)
        {
            var perfil = await _roleManager.FindByIdAsync(model.Id);

            if (perfil == null)
            {
                ViewBag.ErrorMessage = $"Perfil com Id = {model.Id} não foi encontrado";
                return View("NotFound");
            }
            else
            {
                perfil.Name = model.NomePerfil;
                
                var result = await _roleManager.UpdateAsync(perfil);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListarPerfis");
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
      
            var perfil = await _roleManager.FindByIdAsync(roleId);
            if (perfil == null)
            {
                ViewBag.ErrorMessage = $"Perfil com Id = {roleId} não foi encontrado.";
                return View("NotFound");
            }

            var model = new List<UsuarioPerfilViewModel>();
            var listaUsuarios = _userManager.Users.ToList();
            var perfilSupervisor = _roleManager.Roles.Where(ps => ps.TipoPerfil == TipoPerfil.SUPERVISOR).First();
            var perfilAtendente = _roleManager.Roles.Where(pa => pa.TipoPerfil == TipoPerfil.ATENDENTE).First();

            foreach (var user in listaUsuarios)
            {
                if (perfilSupervisor.Id == perfil.Id) 
                {
                    if (await _userManager.IsInRoleAsync(user, perfilSupervisor.Name))
                    {
                        var usuarioNoPerfil = new UsuarioPerfilViewModel
                        {
                            UsuarioId = user.Id,
                            UsuarioEmail = user.Nome +" / "+ user.UserName,
                            IsSelected = true
                        };
                        model.Add(usuarioNoPerfil);
                    }
                    else 
                    {
                        if (!await _userManager.IsInRoleAsync(user, perfilAtendente.Name)) 
                        {
                            var usuarioNoPerfil = new UsuarioPerfilViewModel
                            {
                                UsuarioId = user.Id,
                                UsuarioEmail = user.Nome + " / " + user.UserName,
                                IsSelected = false
                            };
                            model.Add(usuarioNoPerfil);
                        }
                    }
                }

                if (perfilAtendente.Id == perfil.Id)
                {
                    if (await _userManager.IsInRoleAsync(user, perfilAtendente.Name))
                    {
                        var usuarioNoPerfil = new UsuarioPerfilViewModel
                        {
                            UsuarioId = user.Id,
                            UsuarioEmail = user.Nome + " / " + user.UserName,
                            IsSelected = true
                        };
                        model.Add(usuarioNoPerfil);
                    }
                    else
                    {
                        if (!await _userManager.IsInRoleAsync(user, perfilSupervisor.Name))
                        {
                            var usuarioNoPerfil = new UsuarioPerfilViewModel
                            {
                                UsuarioId = user.Id,
                                UsuarioEmail = user.Nome + " / " + user.UserName,
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
            var perfil = await _roleManager.FindByIdAsync(roleId);
            if (perfil == null)
            {
                ViewBag.ErrorMessage = $"Perfil com Id = {roleId} não foi encontrado.";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UsuarioId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, perfil.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, perfil.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, perfil.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, perfil.Name);
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
                        return RedirectToAction("EditarPerfil", new { Id = roleId });
                }
            }
            return RedirectToAction("EditarPerfil", new { Id = roleId });
        }


        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(viewModel);
        }

    }
}
