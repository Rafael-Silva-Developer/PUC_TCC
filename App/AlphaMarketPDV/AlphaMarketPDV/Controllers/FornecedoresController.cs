using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using AlphaMarketPDV.Services;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Services.Exceptions;
using AlphaMarketPDV.Models.ViewModels.FornecedorView;
using AlphaMarketPDV.Models.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

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
        public IActionResult Create(bool? reload, FornecedorViewModel modelReload)
        {
            if (reload == true)
            {
                return View(modelReload);
            }
            else 
            {
                var viewModel = new FornecedorViewModel();
                return View(viewModel);
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fvm)
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
                //return View();
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

        

        [HttpPost]
        public IActionResult AdicionarContato(FornecedorViewModel model)
        {
            int iIdContrato = model.ListaContatos.Count;
            iIdContrato = ++iIdContrato;

            var oContato = new Contato {
                Id = iIdContrato,
                Celular = model.Contato.Celular,
                Email = model.Contato.Email,
                Ramal = model.Contato.Ramal,
                Telefone = model.Contato.Telefone
            };

            var oFornecedor = new Fornecedor
            {
                Id = 0,
                Ativo = model.Fornecedor.Ativo,
                TipoEmpresa = model.Fornecedor.TipoEmpresa,
                Site = model.Fornecedor.Site,
                EndComplemento = model.Fornecedor.EndComplemento,
                EnderecoId = 0,
                EndNumero = model.Fornecedor.EndNumero,
                InscrEstadual = model.Fornecedor.InscrEstadual,
                InscrMunicipal = model.Fornecedor.InscrMunicipal,
                NomeFantasia = model.Fornecedor.NomeFantasia,
                NomeRepresentante = model.Fornecedor.NomeRepresentante,
                NumDocumento = model.Fornecedor.NumDocumento,
                Observacoes = model.Fornecedor.Observacoes,
                RazaoSocial = model.Fornecedor.RazaoSocial
            };

            var modelAux = new FornecedorViewModel();
            modelAux.Fornecedor = oFornecedor;
            modelAux.Contato = oContato;
            modelAux.ListaContatos.Add(oContato);
            /*
            modelAux.Contato.Celular = string.Empty;
            modelAux.Contato.Email = string.Empty;
            modelAux.Contato.Fornecedor = null;
            modelAux.Contato.FornecedorId = 0;
            modelAux.Contato.Id = 0;
            modelAux.Contato.Ramal = 0;
            modelAux.Contato.Telefone = string.Empty;
            */

            //var iIdContato = model.Fornecedor.Contatos.Count;
            //iIdContato = ++iIdContato;

            /*
            model.Contato.Id = iIdContato;
            model.Contato.Fornecedor = model.Fornecedor;

            model.Fornecedor.AdicionarContato(model.Contato);

            model.Contato.Celular = string.Empty;
            model.Contato.Email = string.Empty;
            model.Contato.Fornecedor = null;
            model.Contato.FornecedorId = 0;
            model.Contato.Id = 0;
            model.Contato.Ramal = 0;
            model.Contato.Telefone = string.Empty;
            */
            //RedirectToRoute(nameof(Create) );
            return View("Create", modelAux);
            //return RedirectToAction(nameof(Create), new { @reload=true, @modelReload = modelAux });
           //return RedirectToAction(nameof(Create), new RouteValueDictionary());
            //RedirectToRoute(nameof(Create), new { @reload = true, @modelReload = modelAux });
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

        public PartialViewResult AddContatoFornecedor() 
        {
            return PartialView();
        }


        [HttpGet]
        public IActionResult Error(string message, int codigoErro)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Codigo = codigoErro };
            return View(viewModel);
        }  
    }
}
