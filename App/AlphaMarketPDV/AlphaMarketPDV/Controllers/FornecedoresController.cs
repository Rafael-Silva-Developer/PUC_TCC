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
    public class FornecedoresController : Controller
    {
        private readonly FornecedorService _fornecedorService;

        public FornecedoresController(FornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var fornecedores = await _fornecedorService.ListarTodosFornecedoresAsync();
            return View(fornecedores);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new FornecedorViewModel { Fornecedor = null, Contatos = null, Endereco = null };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fornecedor fornecedor)
        {
            if (!ModelState.IsValid)
            {
               // var categorias = await _categoriaService.ListarTodosAsync();
                //var unidadesMedida = await _unidadeMedidaService.ListarTodosAsync();
                //var viewModel = new ProdutoViewModel { Produto = produto, Categorias = categorias, UnidadesMedida = unidadesMedida };
                return View();
            }

           // if (_produtoService.CodigoProdutoExistente(produto))
            {
                //TempData["Message"] = "Já existe um produto cadastrado com esse código!";

                //var categorias = await _categoriaService.ListarTodosAsync();
                //var unidadesMedida = await _unidadeMedidaService.ListarTodosAsync();
                //var viewModel = new ProdutoViewModel { Produto = produto, Categorias = categorias, UnidadesMedida = unidadesMedida };
                return View();
            }

            //if (produto.FotoProduto != null)
            {
                //string nomeFotoProduto = _produtoService.UploadImagemProduto(produto);
                //produto.Foto = nomeFotoProduto;
            }

            //DateTime data = DateTime.Now;
            //produto.DataHoraCadastro = data;

            //await _produtoService.InserirAsync(produto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para exclusão do fornecedor!", codigoErro = 404 });
            }
           
            var fornecedor = await _fornecedorService.ListarFornecedorPorIdAsync(id.Value);
            if (fornecedor == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Fornecedor não encontrado para exclusão!", codigoErro = 404 });
            }
            return View(fornecedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _fornecedorService.RemoverFornecedorAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para visualização do fornecedor!", codigoErro = 404 });
            }

            var fornecedor = await _fornecedorService.ListarFornecedorPorIdAsync(id.Value);
            if (fornecedor == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Fornecedor não encontrado para visualização!", codigoErro = 404 });
            }

            return View(fornecedor);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para edição do fornecedor!", codigoErro = 404 });
            }

            var fornecedorObj = await _fornecedorService.ListarFornecedorPorIdAsync(id.Value);
            if (fornecedorObj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para edição do fornecedor!", codigoErro = 404 });
            }

            //var categorias = await _categoriaService.ListarTodosAsync();
            //var unidadesMedida = await _unidadeMedidaService.ListarTodosAsync();
            //var viewModel = new ProdutoViewModel { Produto = obj, Categorias = categorias, UnidadesMedida = unidadesMedida };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            if (!ModelState.IsValid)
            {
                //var categorias = await _categoriaService.ListarTodosAsync();
                //var unidadesMedida = await _unidadeMedidaService.ListarTodosAsync();
                //var viewModel = new ProdutoViewModel { Produto = produto, Categorias = categorias, UnidadesMedida = unidadesMedida };
                return View();
            }

            if (id != produto.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "O Id informado na requisição não corresponde ao fornecedor selecionado para edição!", codigoErro = 404 });
            }

            try
            {
                //Produto produtoAux = await _produtoService.ListarPorIdNoTrackingAsync(id);

                //if (produto.Codigo != produtoAux.Codigo)
                {
                   // if (_produtoService.CodigoProdutoExistente(produto))
                    {
                        TempData["Message"] = "Já existe um produto cadastrado com esse código!";

                        //var categorias = await _categoriaService.ListarTodosAsync();
                        //var unidadesMedida = await _unidadeMedidaService.ListarTodosAsync();
                        //var viewModel = new ProdutoViewModel { Produto = produto, Categorias = categorias, UnidadesMedida = unidadesMedida };
                        return View();
                    }
                }

                if (produto.FotoProduto != null)
                {
                    //string nomeFotoProduto = _produtoService.UploadImagemProduto(produto);
                    //_produtoService.ExcluirImagemProduto(produto);
                   // produto.Foto = nomeFotoProduto;
                }

                //await _produtoService.UpdateAsync(produto);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }








        [HttpGet]
        public IActionResult Error(string message, int codigoErro)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Codigo = codigoErro };
            return View(viewModel);
        }  
    }
}
