using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using AlphaMarketPDV.Services;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Services.Exceptions;
using AlphaMarketPDV.Models.ViewModels.Estoque;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace AlphaMarketPDV.Controllers
{
    [Produces("application/json")]
    [Authorize(Roles = "Supervisor")]
    public class EstoqueController : Controller
    {
        private readonly EstoqueService _estoqueService;
        private readonly FornecedorService _fornecedorService;
        private readonly ProdutoService _produtoService;
        private readonly UsuarioManagerService _usuarioManagerService;
        private readonly ManutencaoService _manutencaoService;

        public EstoqueController(EstoqueService estoqueService, FornecedorService fornecedorService, ProdutoService produtoService,
                                 UsuarioManagerService usuarioManagerService, ManutencaoService manutencaoService)
        {
            _estoqueService = estoqueService;
            _fornecedorService = fornecedorService;
            _produtoService = produtoService;
            _usuarioManagerService = usuarioManagerService;
            _manutencaoService = manutencaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lstProdutosEstoque = await _estoqueService.GetProdutosEstoqueAsync();
            return View(lstProdutosEstoque);
        }

        [HttpGet]
        public async Task<IActionResult> Entrada()
        {
            var lstFornecedores = await _fornecedorService.GetFornecedoresAsync();
            var viewModel = new EntradaViewModel { ListaFornecedores = lstFornecedores };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Saida()
        {
            var viewModel = new SaidaViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CarregarProduto(string codigo)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Entrada([FromBody] EntradaViewModel fvm)
        {
            if ((!ModelState.IsValid) || (fvm == null))
            {
                return null;
            }

            var objFornecedor = await _fornecedorService.GetFornecedorPorIdAsync(fvm.EntradaEstoque.FornecedorId);
            var objUsuario = await _usuarioManagerService.GetUsuarioLogadoAynsc();

            var oEntradaEstoque = new EntradaEstoque
            {
                DataHora = fvm.EntradaEstoque.DataHora,
                ValorTotal = fvm.EntradaEstoque.ValorTotal,
                Observacao = fvm.EntradaEstoque.Observacao,
                DataHoraInformada = DateTime.Now,
                Fornecedor = objFornecedor,
                FornecedorId = fvm.EntradaEstoque.FornecedorId,
                Usuario = objUsuario,
                UsuarioId = objUsuario.Id,
                ItensEntradaEstoque = null,
                IdentificadorRegistro = null
            };

            //Gero um hash com os dados do objeto de entrada para identifica-lo antes da persistencia...
            oEntradaEstoque.IdentificadorRegistro = _manutencaoService.GerarMD5(oEntradaEstoque.ToString());

            //Realizo a inclusão...
            await _estoqueService.InserirEntradaAsync(oEntradaEstoque);

            //Recupero o objeto já totalmente identificado...
            var objEntrada = await _estoqueService.GetEntradaPorIdentificadorAsync(oEntradaEstoque.IdentificadorRegistro);

            Produto objProduto;

            //Criar e persistir os itens da entrada...
            foreach (var item in fvm.EntradaEstoque.ItensEntradaEstoque)
            {
                objProduto = null;
                if (item != null) {
                    objProduto = await _produtoService.GetProdutoPorIdAsync(item.ProdutoId);

                    if (objProduto != null) {
                        await _estoqueService.InserirItemEntradaAsync(
                        new ItemEntradaEstoque
                        {
                            NrSeq = item.NrSeq,
                            Qtd = item.Qtd,
                            ValorUnitario = (item.ValorItem / item.Qtd),
                            ValorItem = item.ValorItem,
                            EntradaEstoque = objEntrada,
                            EntradaEstoqueId = objEntrada.Id,
                            Produto = objProduto,
                            ProdutoId = item.Id,
                        });
                        await _estoqueService.AtualizarEstoqueAsync(item.ProdutoId, item.Qtd, "C");
                    }
                }              
            }

            return Json("OK");           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Saida([FromBody] SaidaViewModel fvm)
        {
            if ((!ModelState.IsValid) || (fvm == null))
            {
                return null;
            }

            var oUsuario = await _usuarioManagerService.GetUsuarioLogadoAynsc();
            var oSaidaEstoque = new SaidaEstoque
            {
                DataHora = DateTime.Now,
                Justificativa = fvm.SaidaEstoque.Justificativa,
                DataHoraInformada = fvm.SaidaEstoque.DataHoraInformada,
                ValorTotal = fvm.SaidaEstoque.ValorTotal,
                Usuario = oUsuario,
                UsuarioId = oUsuario.Id,
                ItensSaidaEstoque = null,
                IdentificadorRegistro = null
            };

            //Gero um hash com os dados do objeto de saída para identifica-lo antes da persistencia...
            oSaidaEstoque.IdentificadorRegistro = _manutencaoService.GerarMD5(oSaidaEstoque.ToString());

            //Realizo a inclusão...
            await _estoqueService.InserirSaidaAsync(oSaidaEstoque);

            //Recupero o objeto já totalmente identificado...
            var objSaida = await _estoqueService.GetSaidaPorIdentificadorAsync(oSaidaEstoque.IdentificadorRegistro);

            Produto itemProduto;

            //Criar e persistir os itens da saída...
            foreach (var item in fvm.SaidaEstoque.ItensSaidaEstoque) 
            {
                itemProduto = null;
                if (item != null) 
                {
                    itemProduto = await _produtoService.GetProdutoPorIdAsync(item.ProdutoId);

                    if (itemProduto != null) 
                    {
                        await _estoqueService.InserirItemSaidaAsync(
                            new ItemSaidaEstoque {
                                NrSeq = item.NrSeq,
                                Valor = item.Valor,
                                Qtd = item.Qtd,
                                ValorTotal = item.ValorTotal,
                                Produto = itemProduto,
                                ProdutoId = itemProduto.Id,
                                SaidaEstoque = objSaida,
                                SaidaEstoqueId = objSaida.Id
                            });
                        await _estoqueService.AtualizarEstoqueAsync(item.ProdutoId, item.Qtd, "D");
                    }
                }
            }           
            return Json("OK");
        }







    }
}
