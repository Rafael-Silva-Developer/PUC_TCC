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
using System.Linq;

namespace AlphaMarketPDV.Controllers
{
    [Authorize(Roles = "Supervisor")]
    public class FornecedoresController : Controller
    {
        private readonly FornecedorService _fornecedorService;
        private readonly EnderecoService _enderecoService;

        public FornecedoresController(FornecedorService fornecedorService, EnderecoService enderecoService)
        {
            _fornecedorService = fornecedorService;
            _enderecoService = enderecoService;
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
            ViewBag.lstContatos = new List<Contato>();

            var viewModel = new FornecedorViewModel();
            return View(viewModel);
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

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AdicionarContato(string telefone, int ramal, string celular, string email, int qtdLinhas)
        {
            int iIdContrato = qtdLinhas;
            iIdContrato = ++iIdContrato;

            var oContato = new Contato
            {
                Id = iIdContrato,
                Celular = celular,
                Email = email,
                Ramal = ramal,
                Telefone = telefone,
                Fornecedor = null,
                FornecedorId = 0
            };
            ViewBag.lstContatos = new List<Contato>();
            ViewBag.lstContatos.Add(oContato);

            return Json(oContato);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdicionarContato(FornecedorViewModel fvm)
        {
            var id = fvm.ListaContatos.Count();
            id = ++id;
            var objContato = new Contato { 
                Id = id,
                Celular = fvm.Contato.Celular,
                Email = fvm.Contato.Email,
                Ramal = fvm.Contato.Ramal,
                Telefone = fvm.Contato.Telefone,
                Fornecedor = null,
                FornecedorId = 0
            };

            fvm.ListaContatos.Add(objContato);

            /*int iIdContrato = qtdLinhas;
            iIdContrato = ++iIdContrato;

            var oContato = new Contato
            {
                Id = iIdContrato,
                Celular = celular,
                Email = email,
                Ramal = ramal,
                Telefone = telefone,
                Fornecedor = null,
                FornecedorId = 0
            };
            ViewBag.lstContatos = new List<Contato>();
            ViewBag.lstContatos.Add(oContato);*/

            return RedirectToAction("Create", fvm);
        }


        public IQueryable<FornecedorViewModel> IncluirContato(FornecedorViewModel model)
        {
            ViewBag.lstContatos.Add(model.Contato);
            return ViewBag.lstContatos;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> BuscarCep(string cep)
        {
            if (cep != null)
            {
                var aux = cep.Replace("-", "");
                aux = aux.Replace(".", "");
                Endereco oEndereco = await _enderecoService.ListarEnderecoPorCepAsync(aux);
                if (oEndereco != null)
                {
                    var EnderecoAux = new Endereco()
                    {
                        Id = oEndereco.Id,
                        Bairro = oEndereco.Bairro,
                        Cep = oEndereco.Cep,
                        Cidade = oEndereco.Cidade,
                        Lougradouro = oEndereco.Lougradouro,
                        Uf = oEndereco.Uf
                    };

                    return Json(EnderecoAux);
                }
                else
                {
                    var correios = new API_Correios.AtendeClienteClient();
                    var consulta = correios.consultaCEPAsync(cep.Replace("-", "")).Result;

                    if (consulta != null)
                    {
                        var oNewEndereco = new Endereco()
                        {
                            Bairro = consulta.@return.bairro,
                            Cep = consulta.@return.cep,
                            Cidade = consulta.@return.cidade,
                            Lougradouro = consulta.@return.end,
                            Uf = consulta.@return.uf
                        };

                        await _enderecoService.InserirEnderecoAsync(oNewEndereco);
                        return Json(oNewEndereco);
                    }
                    else
                    {
                        return Json(null);
                    }
                }
            }
            else
            {
                return Json(null);
            }
        }




    }
}
