using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using AlphaMarketPDV.Services;
using AlphaMarketPDV.Models.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace AlphaMarketPDV.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ManutencaoService _manutencaoService;
        private readonly FluxoCaixaService _fluxoCaixaService;

        public HomeController(ManutencaoService manutencaoService, FluxoCaixaService fluxoCaixaService)
        {
            _manutencaoService = manutencaoService;
            _fluxoCaixaService = fluxoCaixaService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            ViewData["Message"] = "Sobre o autor.";

            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContatoViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else 
            {
                try
                {
                    var result = await _manutencaoService.EnviarEmailContato(model);
                    if (result)
                    {
                        ViewBag.Mensagem = "Email enviado com sucesso!";
                        ViewBag.Tipo = 0;
                        model.Email = string.Empty;
                        model.Mensagem = string.Empty;
                        model.Nome = string.Empty;
                        model.Sobrenome = string.Empty;
                        return View(model);                      
                    }
                    else
                    {
                        ViewBag.Mensagem = "Falha ao enviar Email!";
                        ViewBag.Tipo = 1;
                        return View(model);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = $"Erro ao Tentar enviar Email: {e.Message}";
                    ViewBag.Tipo = 1;
                    return View(model);
                }                           
            }    
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> VerificarCaixaAberto()
        {           
            var resp = await _fluxoCaixaService.VerificarCaixaAbertoAsync();
            return Json(resp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> VerificarCaixaFechado()
        {
            var resp = await _fluxoCaixaService.VerificarCaixaFechadoAsync();
            return Json(resp);
        }









    }
}
