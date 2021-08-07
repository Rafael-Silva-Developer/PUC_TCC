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
    public class UnidadesMedidaController : Controller
    {

        private readonly UnidadeMedidaService _unidadeMedidaService;

        public UnidadesMedidaController(UnidadeMedidaService unidadeMedidaService)
        {
            this._unidadeMedidaService = unidadeMedidaService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _unidadeMedidaService.ListarTodosAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UnidadeMedida unidadeMedida)
        {
            if (!ModelState.IsValid)
            {
                return View(unidadeMedida);
            }

            await _unidadeMedidaService.InserirAsync(unidadeMedida);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para exclusão da unidade de medida!" });
            }

            var obj = await _unidadeMedidaService.ListarPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para exclusão da unidade de medida!" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unidadeMedidaService.RemoverAsync(id);
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
                return RedirectToAction(nameof(Error), new { message = "Id não informado para visualização da unidade de medida!" });
            }

            var obj = await _unidadeMedidaService.ListarPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para visualização da unidade de medida!" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para edição da unidade de medida!" });
            }

            var obj = await _unidadeMedidaService.ListarPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para edição da unidade de medida!" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UnidadeMedida unidadeMedida)
        {
            if (!ModelState.IsValid)
            {
                return View(unidadeMedida);
            }

            if (id != unidadeMedida.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id informado na requisição não corresponde a unidade de medida selecionada para edição!" });
            }

            try
            {
                await _unidadeMedidaService.UpdateAsync(unidadeMedida);
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
