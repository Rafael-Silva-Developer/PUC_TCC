using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AlphaMarketPDV.Services;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Models.ViewModels.Venda;

namespace AlphaMarketPDV.Controllers
{
    [Produces("application/json")]
    public class VendasController : Controller
    {
        private readonly EstoqueService _estoqueService;
        private readonly FornecedorService _fornecedorService;
        private readonly ProdutoService _produtoService;
        private readonly UsuarioManagerService _usuarioManagerService;
        private readonly ManutencaoService _manutencaoService;
        private readonly FormaPagamentoService _formaPagamentoService;

        public VendasController(EstoqueService estoqueService, FornecedorService fornecedorService, ProdutoService produtoService,
                                UsuarioManagerService usuarioManagerService, ManutencaoService manutencaoService,
                                FormaPagamentoService formaPagamentoService) 
        {
            _estoqueService = estoqueService;
            _fornecedorService = fornecedorService;
            _produtoService = produtoService;
            _usuarioManagerService = usuarioManagerService;
            _manutencaoService = manutencaoService;
            _formaPagamentoService = formaPagamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> ProcessarVenda()
        {
            var lstFormaPag = await _formaPagamentoService.GetFormasPagamentoAsync();
            var viewModel = new VendaViewModel { ListaFormasPagamento = lstFormaPag };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> PesquisarProduto(string codigo)
        {
            if (codigo != null)
            {
                Produto oProduto = await _produtoService.GetProdutoPorCodigoAsync(codigo);
                if (oProduto != null)
                {
                    return Json(oProduto);
                }
                else
                {
                    return Json(null);
                }
            }
            else
            {
                return Json(null);
            }
        }



    }
}
