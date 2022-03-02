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
    [Produces("application/json")]
    [Authorize(Roles = "Supervisor")]
    public class FornecedoresController : Controller
    {
        private readonly FornecedorService _fornecedorService;
        private readonly EnderecoService _enderecoService;
        private readonly ContatoService _contatoService;

        public FornecedoresController(FornecedorService fornecedorService, EnderecoService enderecoService, 
                                      ContatoService contatoService)
        {
            _fornecedorService = fornecedorService;
            _enderecoService = enderecoService;
            _contatoService = contatoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var fornecedores = await _fornecedorService.GetFornecedoresAsync();
            return View(fornecedores);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new FornecedorViewModel();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para exclusão do fornecedor!", codigoErro = 404 });
            }

            var fornecedor = await _fornecedorService.GetFornecedorPorIdAsync(id.Value);
            if (fornecedor == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Fornecedor não encontrado para exclusão!", codigoErro = 404 });
            }
            return View(fornecedor);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado para visualização do fornecedor!", codigoErro = 404 });
            }

            var fornecedor = await _fornecedorService.GetFornecedorPorIdAsync(id.Value);
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

            var fornecedorObj = await _fornecedorService.GetFornecedorPorIdAsync(id.Value);
            if (fornecedorObj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado para edição do fornecedor!", codigoErro = 404 });
            }

            var oEndereco = await _enderecoService.ListarEnderecoPorIdAsync(fornecedorObj.EnderecoId);
            var oContatos = await _contatoService.ListarTodosContatosFornecedorIdAsync(fornecedorObj.Id);
            var viewModel = new FornecedorViewModel { Fornecedor = fornecedorObj, Contato = null, ListaContatos = oContatos };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Error(string message, int codigoErro)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Codigo = codigoErro };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([FromBody] FornecedorViewModel fvm)
        {
            if ((!ModelState.IsValid) || (fvm == null))
            {
                return null;
            }

            var oFornecedor = new Fornecedor
            {
                RazaoSocial = fvm.Fornecedor.RazaoSocial,
                NomeFantasia = fvm.Fornecedor.NomeFantasia,
                NomeRepresentante = fvm.Fornecedor.NomeRepresentante,
                InscrEstadual = fvm.Fornecedor.InscrEstadual,
                InscrMunicipal = fvm.Fornecedor.InscrMunicipal,
                EndComplemento = fvm.Fornecedor.EndComplemento,
                EndNumero = fvm.Fornecedor.EndNumero,
                NumDocumento = fvm.Fornecedor.NumDocumento,
                Observacoes = fvm.Fornecedor.Observacoes,
                Ativo = fvm.Fornecedor.Ativo,
                TipoEmpresa = fvm.Fornecedor.TipoEmpresa,
                Site = fvm.Fornecedor.Site
            };

            var oEndereco = await _enderecoService.ListarEnderecoPorIdAsync(fvm.Fornecedor.EnderecoId);
            oFornecedor.Endereco = oEndereco;
            await _fornecedorService.InserirFornecedorAsync(oFornecedor);

            var oFornecedorAux = await _fornecedorService.ListarFornecedorPorRazaoSocialAsync(oFornecedor.RazaoSocial);

            foreach (var contato in fvm.Fornecedor.Contatos)
            {
                await _contatoService.InserirContatoAsync(new Contato
                {
                    Celular = contato.Celular,
                    Email = contato.Email,
                    Ramal = contato.Ramal,
                    Telefone = contato.Telefone,
                    Fornecedor = oFornecedorAux,
                    NrSeq = contato.NrSeq
                });
            }

            return Json("OK");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _fornecedorService.RemoverFornecedorAsync(id);
                await _contatoService.RemoverContatosFornecedorAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit([FromBody] FornecedorViewModel fvm)
        {           
            if ((!ModelState.IsValid) || (fvm == null))
            {
                return null;
            }

            var oFornecedor = new Fornecedor
            {
                Id = fvm.Fornecedor.Id,
                RazaoSocial = fvm.Fornecedor.RazaoSocial,
                NomeFantasia = fvm.Fornecedor.NomeFantasia,
                NomeRepresentante = fvm.Fornecedor.NomeRepresentante,
                InscrEstadual = fvm.Fornecedor.InscrEstadual,
                InscrMunicipal = fvm.Fornecedor.InscrMunicipal,
                EndComplemento = fvm.Fornecedor.EndComplemento,
                EndNumero = fvm.Fornecedor.EndNumero,
                NumDocumento = fvm.Fornecedor.NumDocumento,
                Observacoes = fvm.Fornecedor.Observacoes,
                Ativo = fvm.Fornecedor.Ativo,
                TipoEmpresa = fvm.Fornecedor.TipoEmpresa,
                Site = fvm.Fornecedor.Site
            };

            var oEndereco = await _enderecoService.ListarEnderecoPorIdAsync(fvm.Fornecedor.EnderecoId);
            oFornecedor.Endereco = oEndereco;
            await _fornecedorService.AtualizarFornecedorAsync(oFornecedor);

            //Busco todos os contatos do fornecedor já cadastrados...
            var lstContatos = await _contatoService.ListarTodosContatosFornecedorIdAsync(oFornecedor.Id);
         
            var lstContatosExcluidos = lstContatos.Except(fvm.ListaContatos);
            if (lstContatosExcluidos.Count() == 1) 
            {
                foreach (var item in lstContatosExcluidos) 
                {
                    await _contatoService.RemoverContatoAsync(item.Id);
                }
            }
           
            //Atualização ou inclusão de contatos...
            foreach (var itemModel in fvm.Fornecedor.Contatos)
            {
                if (itemModel != null)
                {
                    if (itemModel.Id == 0)
                    {
                        await _contatoService.InserirContatoAsync(new Contato
                        {
                            NrSeq = itemModel.NrSeq,
                            Celular = itemModel.Celular,
                            Email = itemModel.Email,
                            Ramal = itemModel.Ramal,
                            Telefone = itemModel.Telefone,
                            Fornecedor = oFornecedor
                        });
                    }
                    else
                    {
                        await _contatoService.AtualizarContatoAsync(new Contato
                        {
                            Id = itemModel.Id,
                            NrSeq = itemModel.NrSeq,
                            Celular = itemModel.Celular,
                            Email = itemModel.Email,
                            Ramal = itemModel.Ramal,
                            Telefone = itemModel.Telefone,
                            Fornecedor = oFornecedor
                        });
                    }
                }
            }
            return Json("OK");
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
