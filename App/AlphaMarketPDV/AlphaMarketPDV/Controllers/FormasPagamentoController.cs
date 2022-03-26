using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AlphaMarketPDV.Services;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Services.Exceptions;
using AlphaMarketPDV.Models.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace AlphaMarketPDV.Controllers
{
    [Authorize(Roles = "Supervisor")]
    public class FormasPagamentoController : Controller
    {
        private readonly FormaPagamentoService _formaPagamentoService;

        public FormasPagamentoController(FormaPagamentoService formaPagamentoService) 
        {
            this._formaPagamentoService = formaPagamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _formaPagamentoService.GetFormasPagamentoAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FormaPagamento obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            if (_formaPagamentoService.GetDescricaoFormaPagamentoExistente(obj)) 
            {
                TempData["Message"] = "Já existe uma forma de pagamento cadastrada com essa descrição!";
                return View(obj);
            }

            await _formaPagamentoService.InserirAsync(obj);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para exclusão da forma de pagamento!" });
            }

            var obj = await _formaPagamentoService.GetFormaPagamentoPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para exclusão da forma de pagamento!" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _formaPagamentoService.RemoverAsync(id);
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
                return RedirectToAction(nameof(Error), new { message = "Id não informado para visualização da forma de pagamento!" });
            }

            var obj = await _formaPagamentoService.GetFormaPagamentoPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para visualização da forma de pagamento!" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para edição da forma de pagamento!" });
            }

            var obj = await _formaPagamentoService.GetFormaPagamentoPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para edição da forma de pagamento!" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FormaPagamento obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            if (id != obj.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id informado na requisição não corresponde a forma de pagamento selecionada para edição!" });
            }

            try
            {
                FormaPagamento formaPag = await _formaPagamentoService.GetFormaPagamentoPorIdNoTrackingAsync(id);
                if (obj.Descricao.ToUpper() != formaPag.Descricao.ToUpper()) 
                {
                    if (_formaPagamentoService.GetDescricaoFormaPagamentoExistente(obj))
                    {
                        TempData["Message"] = "Já existe uma forma de pagamento cadastrada com essa descrição!";
                        return View(obj);
                    }
                }

                await _formaPagamentoService.UpdateAsync(obj);
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
