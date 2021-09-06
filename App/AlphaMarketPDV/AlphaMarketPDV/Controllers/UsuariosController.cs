using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AlphaMarketPDV.Services;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Services.Exceptions;
using AlphaMarketPDV.Models.ViewModels;
using System.Diagnostics;

namespace AlphaMarketPDV.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly LojaService _lojaService;

        public UsuariosController(UsuarioService usuarioService, LojaService lojaService) 
        {
            _usuarioService = usuarioService;
            _lojaService = lojaService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _usuarioService.ListarTodosAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var lojas = await _lojaService.ListarTodosAsync();
            var viewModel = new UsuarioFormViewModel { ListaLojas = lojas };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                var lojas = await _lojaService.ListarTodosAsync();
                var viewModel = new UsuarioFormViewModel { Usuario = usuario, ListaLojas = lojas };
                return View(viewModel);
            }

            if (_usuarioService.LoginExistente(usuario))
            {
                TempData["Message"] = "Já existe um usuário com esse login!";
                var lojas = await _lojaService.ListarTodosAsync();
                var viewModel = new UsuarioFormViewModel { Usuario = usuario, ListaLojas = lojas };
                return View(viewModel);
            }

            if (usuario.FotoUsuarioLoad != null)
            {
                string nomeFotoProduto = _usuarioService.UploadImagemUsuario(usuario);
                usuario.FotoUsuario = nomeFotoProduto;
            }

            await _usuarioService.InserirAsync(usuario);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para exclusão do usuario!" });
            }

            var usuario = await _usuarioService.ListarPorIdAsync(id.Value);
            if (usuario == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para exclusão do usuário!" });
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _usuarioService.RemoverAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para visualização do usuário!" });
            }

            var usuario = await _usuarioService.ListarPorIdAsync(id.Value);
            if (usuario == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para visualização do usuário!" });
            }

            return View(usuario);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para edição do usuário!" });
            }

            var usuario = await _usuarioService.ListarPorIdAsync(id.Value);
            if (usuario == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para edição do usuário!" });
            }

            var lojas = await _lojaService.ListarTodosAsync();
            var viewModel = new UsuarioFormViewModel { Usuario = usuario, ListaLojas = lojas };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            if (id != usuario.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id informado na requisição não corresponde ao usuário selecionado para edição!" });
            }

            try
            {
                Usuario usuarioAux = await _usuarioService.ListarPorIdNoTrackingAsync(id);
                if (usuario.Login.ToUpper() != usuarioAux.Login.ToUpper())
                {
                    if (_usuarioService.LoginExistente(usuario))
                    {
                        TempData["Message"] = "Já existe um usuário cadastrado com esse login!";
                        return View(usuario);
                    }
                }

                await _usuarioService.UpdateAsync(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(viewModel);
        }
    }
}
