using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using AlphaMarketPDV.Services;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Services.Exceptions;
using AlphaMarketPDV.Models.ViewModels.FluxoCaixa;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using AlphaMarketPDV.Models.ViewModels;

namespace AlphaMarketPDV.Controllers
{
    [Produces("application/json")]
    public class FluxoCaixaController : Controller
    {
        private readonly FluxoCaixaService _fluxoCaixaService;
        private readonly UsuarioManagerService _usuarioManagerService;
        private readonly ManutencaoService _manutencaoService;

        public FluxoCaixaController (FluxoCaixaService fluxoCaixaService, UsuarioManagerService usuarioManagerService,
            ManutencaoService manutencaoService) 
        {
            _fluxoCaixaService = fluxoCaixaService;
            _usuarioManagerService = usuarioManagerService;
            _manutencaoService = manutencaoService;
        }

        [HttpGet]
        public IActionResult Abertura()
        {
            var viewModel = new FluxoCaixaGenericViewModel { DataHora = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")) };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Fechamento()
        {
            var viewModel = new FluxoCaixaGenericViewModel { DataHora = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")) };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Operar()
        {
            var viewModel = new FluxoCaixaViewModel { DataHora = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")) };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Sangria()
        {
            var viewModel = new FluxoCaixaGenericViewModel { DataHora = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")) };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AcompanharFluxoCaixa()
        {
            return View();
        }

        public async Task<IActionResult> BuscarOperacoesCaixa(DateTime? dataIni, DateTime? dataFim)
        {
            if (!dataIni.HasValue)
            {
                dataIni = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            }
            else
            {
                dataIni = new DateTime(dataIni.Value.Year, dataIni.Value.Month, dataIni.Value.Day, 0, 0, 0);
            }

            if (!dataFim.HasValue)
            {
                dataFim = DateTime.Now;
            }
            else
            {
                dataFim = new DateTime(dataFim.Value.Year, dataFim.Value.Month, dataFim.Value.Day, 23, 59, 59);
            }

            ViewData["DataIni"] = dataIni.Value.ToString("yyyy-MM-dd");
            ViewData["DataFim"] = dataFim.Value.ToString("yyyy-MM-dd");
            var result = await _fluxoCaixaService.GetOperacaoesCaixaPorPeriodoAsync(dataIni, dataFim);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> ObterDetalheCaixa(int? idCaixa)
        {
            if (idCaixa == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id do caixa não informado!" });
            }

            var lstItens = await _fluxoCaixaService.GetItensCaixaPorIdCaixaAsync(idCaixa.Value);
            if (lstItens == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Detalhe não encontrado para o id do caixa informado!" });
            }

            return View(lstItens);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Abertura(FluxoCaixaGenericViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            var objUsuario = await _usuarioManagerService.GetUsuarioLogadoAynsc();
            var oCaixa = new Caixa
            {
                DataHora = obj.DataHora,
                CaixaPagamentos = null,
                Valor = obj.Valor,
                TipoOperacao = Models.Enums.TipoCaixa.ABERTURA,
                Usuario = objUsuario,
                UsuarioId = objUsuario.Id
            };

            oCaixa.IdentificadorRegistro = _manutencaoService.GerarMD5(oCaixa.ToString());
            await _fluxoCaixaService.InserirCaixaAsync(oCaixa);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Fechamento(FluxoCaixaGenericViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            var objUsuario = await _usuarioManagerService.GetUsuarioLogadoAynsc();
            var oCaixa = new Caixa
            {
                DataHora = obj.DataHora,
                CaixaPagamentos = null,
                Valor = obj.Valor,
                TipoOperacao = Models.Enums.TipoCaixa.FECHAMENTO,
                Usuario = objUsuario,
                UsuarioId = objUsuario.Id
            };

            oCaixa.IdentificadorRegistro = _manutencaoService.GerarMD5(oCaixa.ToString());
            await _fluxoCaixaService.InserirCaixaAsync(oCaixa);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Operar(FluxoCaixaViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            var objUsuario = await _usuarioManagerService.GetUsuarioLogadoAynsc();
            var oCaixa = new Caixa {
                DataHora = obj.DataHora,
                CaixaPagamentos = null,
                Valor = obj.Valor,
                TipoOperacao = obj.TipoOperacao,
                Usuario = objUsuario,
                UsuarioId = objUsuario.Id
            };

            oCaixa.IdentificadorRegistro = _manutencaoService.GerarMD5(oCaixa.ToString());
            await _fluxoCaixaService.InserirCaixaAsync(oCaixa);
            return RedirectToAction("Index", "Home");        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sangria(FluxoCaixaGenericViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            var objUsuario = await _usuarioManagerService.GetUsuarioLogadoAynsc();
            var oCaixa = new Caixa
            {
                DataHora = obj.DataHora,
                CaixaPagamentos = null,
                Valor = obj.Valor,
                TipoOperacao = Models.Enums.TipoCaixa.SANGRIA,
                Usuario = objUsuario,
                UsuarioId = objUsuario.Id
            };

            oCaixa.IdentificadorRegistro = _manutencaoService.GerarMD5(oCaixa.ToString());
            await _fluxoCaixaService.InserirCaixaAsync(oCaixa);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Error(string message, int codigoErro)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Codigo = codigoErro };
            return View(viewModel);
        }



    }
}
