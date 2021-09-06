using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Services;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Services.Exceptions;
using AlphaMarketPDV.Models.ViewModels;
using System.Diagnostics;

namespace AlphaMarketPDV.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CategoriaService _categoriaService;

        public CategoriasController(CategoriaService categoriaService) 
        {
            this._categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _categoriaService.ListarTodosAsync();
            return View(list);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria) 
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            if (_categoriaService.DescricaoCategoriaExistente(categoria)) 
            {
                TempData["Message"] = "Já existe uma categoria cadastrada com essa descrição!";
                return View(categoria);
            }

            await _categoriaService.InserirAsync(categoria);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para exclusão da categoria!" });
            }

            var obj = await _categoriaService.ListarPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para exclusão da categoria!" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoriaService.RemoverAsync(id);
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
                return RedirectToAction(nameof(Error), new { message = "Id não informado para visualização da categoria!" });
            }

            var obj = await _categoriaService.ListarPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para visualização da categoria!" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para edição da categoria!" });
            }

            var obj = await _categoriaService.ListarPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para edição da categoria!" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            if (id != categoria.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id informado na requisição não corresponde a categoria selecionado para edição!" });
            }

            try
            {
                Categoria categoriaAux = await _categoriaService.ListarPorIdNoTrackingAsync(id);
                if (categoria.Descricao.ToUpper() != categoriaAux.Descricao.ToUpper()) 
                {
                    if (_categoriaService.DescricaoCategoriaExistente(categoria))
                    {
                        TempData["Message"] = "Já existe uma categoria cadastrada com essa descrição!";
                        return View(categoria);
                    }
                }

                await _categoriaService.UpdateAsync(categoria);
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
