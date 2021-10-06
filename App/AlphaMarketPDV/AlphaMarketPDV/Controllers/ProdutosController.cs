using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using AlphaMarketPDV.Services;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Models.ViewModels;
using AlphaMarketPDV.Services.Exceptions;
using System;
using Microsoft.AspNetCore.Authorization;

namespace AlphaMarketPDV.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly ProdutoService _produtoService;
        private readonly CategoriaService _categoriaService;
        private readonly UnidadeMedidaService _unidadeMedidaService;
        
        public ProdutosController(ProdutoService produtoService, CategoriaService categoriaService, 
                                  UnidadeMedidaService unidadeMedidaService)
        {
            this._produtoService = produtoService;
            this._categoriaService = categoriaService;
            this._unidadeMedidaService = unidadeMedidaService;           
        }

        public async Task<IActionResult> Index()
        {
            var list = await _produtoService.ListarTodosAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var categorias = await _categoriaService.ListarTodosAsync();
            var unidadesMedida = await _unidadeMedidaService.ListarTodosAsync();
            var viewModel = new ProdutoFormViewModel { Categorias = categorias, UnidadesMedida = unidadesMedida };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            if (!ModelState.IsValid) 
            {
                var categorias = await _categoriaService.ListarTodosAsync();
                var unidadesMedida = await _unidadeMedidaService.ListarTodosAsync();
                var viewModel = new ProdutoFormViewModel { Produto = produto, Categorias = categorias, UnidadesMedida = unidadesMedida };
                return View(viewModel);
            }

            if (_produtoService.CodigoProdutoExistente(produto)) 
            {
                TempData["Message"] = "Já existe um produto cadastrado com esse código!";

                var categorias = await _categoriaService.ListarTodosAsync();
                var unidadesMedida = await _unidadeMedidaService.ListarTodosAsync();
                var viewModel = new ProdutoFormViewModel { Produto = produto, Categorias = categorias, UnidadesMedida = unidadesMedida };
                return View(viewModel);
            }

            if (produto.FotoProduto != null) 
            {
                string nomeFotoProduto = _produtoService.UploadImagemProduto(produto);
                produto.Foto = nomeFotoProduto;
            }
                
            DateTime data = DateTime.Now;
            produto.DataHoraCadastro = data;

            await _produtoService.InserirAsync(produto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id) 
        {
            if (id == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para exclusão do produto!" });
            }

            var obj = await _produtoService.ListarPorIdAsync(id.Value);
            if (obj == null) 
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para exclusão do produto!" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) 
        {
            try
            {
                await _produtoService.RemoverAsync(id);
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
                return RedirectToAction(nameof(Error), new { message = "Id não informado para visualização do produto!" });
            }

            var obj = await _produtoService.ListarPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para visualização do produto!" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id) 
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para edição do produto!" });
            }

            var obj = await _produtoService.ListarPorIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para edição do produto!" });
            }

            var categorias = await _categoriaService.ListarTodosAsync();
            var unidadesMedida = await _unidadeMedidaService.ListarTodosAsync();
            var viewModel = new ProdutoFormViewModel { Produto = obj, Categorias = categorias, UnidadesMedida = unidadesMedida };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto) 
        {
            if (!ModelState.IsValid)
            {
                var categorias = await _categoriaService.ListarTodosAsync();
                var unidadesMedida = await _unidadeMedidaService.ListarTodosAsync();
                var viewModel = new ProdutoFormViewModel { Produto = produto, Categorias = categorias, UnidadesMedida = unidadesMedida };
                return View(viewModel);
            }

            if (id != produto.Id) 
            {
                return RedirectToAction(nameof(Error), new { message = "O Id informado na requisição não corresponde ao produto selecionado para edição!" });
            }

            try
            {
                Produto produtoAux = await _produtoService.ListarPorIdNoTrackingAsync(id);

                if (produto.Codigo != produtoAux.Codigo)
                {
                    if (_produtoService.CodigoProdutoExistente(produto)) 
                    {
                        TempData["Message"] = "Já existe um produto cadastrado com esse código!";

                        var categorias = await _categoriaService.ListarTodosAsync();
                        var unidadesMedida = await _unidadeMedidaService.ListarTodosAsync();
                        var viewModel = new ProdutoFormViewModel { Produto = produto, Categorias = categorias, UnidadesMedida = unidadesMedida };
                        return View(viewModel);
                    }
                }

                if (produto.FotoProduto != null) 
                {
                    string nomeFotoProduto = _produtoService.UploadImagemProduto(produto);                          
                    _produtoService.ExcluirImagemProduto(produto);
                    produto.Foto = nomeFotoProduto;
                }
                          
                await _produtoService.UpdateAsync(produto);
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
