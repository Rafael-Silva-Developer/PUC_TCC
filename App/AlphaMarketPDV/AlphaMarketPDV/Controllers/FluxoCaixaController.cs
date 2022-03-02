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

        

        



    }
}
