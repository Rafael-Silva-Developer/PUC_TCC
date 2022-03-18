using Microsoft.AspNetCore.Mvc;
using AlphaMarketPDV.Services;
using System;
using System.Threading.Tasks;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Models.ViewModels.Venda;
using AlphaMarketPDV.Services.Exceptions;
using System.Diagnostics;
using AlphaMarketPDV.Models.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace AlphaMarketPDV.Controllers
{
    [Produces("application/json")]
    public class VendasController : Controller
    {
        private readonly EstoqueService _estoqueService;
        private readonly ProdutoService _produtoService;
        private readonly UsuarioManagerService _usuarioManagerService;
        private readonly ManutencaoService _manutencaoService;
        private readonly FormaPagamentoService _formaPagamentoService;
        private readonly FluxoCaixaService _fluxoCaixaService;
        private readonly VendasService _vendasService;

        public VendasController(EstoqueService estoqueService, ProdutoService produtoService,
                                UsuarioManagerService usuarioManagerService, ManutencaoService manutencaoService,
                                FormaPagamentoService formaPagamentoService, FluxoCaixaService fluxoCaixaService,
                                VendasService vendasService)
        {
            _estoqueService = estoqueService;
            _produtoService = produtoService;
            _usuarioManagerService = usuarioManagerService;
            _manutencaoService = manutencaoService;
            _formaPagamentoService = formaPagamentoService;
            _fluxoCaixaService = fluxoCaixaService;
            _vendasService = vendasService;
        }

        [HttpGet]
        public async Task<IActionResult> ProcessarVenda()
        {
            var lstFormaPag = await _formaPagamentoService.GetFormasPagamentoAsync();
            var viewModel = new VendaViewModel { ListaFormasPagamento = lstFormaPag };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AcompanharVenda()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AcompanharVendaPorGraficos()
        {
            return View();
        }

        public async Task<IActionResult> BuscaVendasRealizadas(DateTime? dataIni, DateTime? dataFim)
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
            var result = await _vendasService.GetVendasPorPeriodoAsync(dataIni, dataFim);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> ObterDetalheVendasRealizadas(int? idVenda)
        {
            if (idVenda == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id da venda não informado!" });
            }

            var lstItens = await _vendasService.GetItensVendidoPorIdVendaAsync(idVenda.Value);
            if (lstItens == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Detalhe não encontrado para id da venda informado!" });
            }

            return View(lstItens);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ObterSaldoDisponivel(int idProduto)
        {
            if (idProduto > 0)
            {
                double qtd = await _estoqueService.GetSaldoEstoqueAsync(idProduto);
                if (qtd > 0)
                {
                    return Json(qtd);
                }
                else
                {
                    return Json(0);
                }
            }
            else
            {
                return Json(null);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ProcessarVenda([FromBody] VendaViewModel fvm)
        {
            if ((!ModelState.IsValid) || (fvm == null))
            {
                return null;
            }

            //Vou usar a mesma data/hora nas operações...
            var DataOperacao = DateTime.Now;

            //Recupero o usuário logado no sistema...
            var oUsuario = await _usuarioManagerService.GetUsuarioLogadoAynsc();

            //Criar, preencher e salvar os dados do caixa...
            var oCaixa = new Caixa
            {
                DataHora = DataOperacao,
                Valor = fvm.TotalPagar,
                TipoOperacao = Models.Enums.TipoCaixa.VENDA,
                Usuario = oUsuario,
                UsuarioId = oUsuario.Id
            };

            //Gero um hash com os dados do objeto para identifica-lo antes da persistencia...
            oCaixa.IdentificadorRegistro = _manutencaoService.GerarMD5(oCaixa.ToString());

            //Realizo a persistência do caixa...
            await _fluxoCaixaService.InserirCaixaAsync(oCaixa);

            //Recupero o caixa...
            var auxCaixa = await _fluxoCaixaService.GetCaixaPorIdentificadorAsync(oCaixa.IdentificadorRegistro);

            //Realizo a persistência das formas de pagamento...
            FormaPagamento oFormaPag;
            foreach (var item in fvm.ItemPagamento)
            {
                oFormaPag = null;
                if (item != null)
                {
                    oFormaPag = await _formaPagamentoService.GetFormaPagamentoPorIdAsync(item.FormaPagamentoId);

                    if (oFormaPag != null)
                    {
                        await _fluxoCaixaService.InserirCaixaPagamentoAsync(
                        new CaixaPagamento
                        {
                            NrSeq = item.NrSeq,
                            ValorPago = item.ValorPago,
                            ValoTroco = item.ValoTroco,
                            Caixa = auxCaixa,
                            CaixaId = auxCaixa.Id,
                            FormaPagamento = oFormaPag,
                            FormaPagamentoId = oFormaPag.Id
                        });
                    };
                };
            };

            //Crio a venda e preencho os atributos...
            var oVenda = new Venda
            {
                DataHora = DataOperacao,
                DataVenda = new DateTime(DataOperacao.Year, DataOperacao.Month, DataOperacao.Day, 00, 00, 00),
                ValorTotal = fvm.TotalVenda,
                ValorDesconto = fvm.Desconto,
                TotalPagar = fvm.TotalPagar,
                Status = Models.Enums.StatusVenda.FINALIZADO,
                Caixa = auxCaixa,
                CaixaId = auxCaixa.Id
            };

            //Gero um hash com os dados do objeto para identifica-lo antes da persistencia...
            oVenda.IdentificadorRegistro = _manutencaoService.GerarMD5(oVenda.ToString());

            //Realizo a persistência do caixa...
            await _vendasService.InserirVendaAsync(oVenda);

            //Recupero a referência da venda...
            var auxVenda = await _vendasService.GetVendaPorIdentificadorAsync(oVenda.IdentificadorRegistro);

            //Realizo a persistência dos itens da venda...
            Produto auxProduto;

            //Criar e persistir os itens da entrada...
            foreach (var item in fvm.ItensVenda)
            {
                auxProduto = null;
                if (item != null)
                {
                    auxProduto = await _produtoService.GetProdutoPorIdAsync(item.ProdutoId);

                    if (auxProduto != null)
                    {
                        await _vendasService.InserirItemVendaAsync(
                        new ItemVenda
                        {
                            NrSeq = item.NrSeq,
                            ValorUnitario = item.ValorUnitario,
                            Qtd = item.Qtd,
                            ValorItem = item.ValorItem,
                            Cancelado = item.Cancelado,
                            Produto = auxProduto,
                            ProdutoId = auxProduto.Id,
                            Venda = auxVenda,
                            VendaId = auxVenda.Id
                        });
                        await _estoqueService.AtualizarEstoqueAsync(item.ProdutoId, item.Qtd, "D");
                    };
                };
            };

            return Json("OK");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GraficoTotalVenda([FromBody] FiltroRelatorioView fvm)
        {
            var dtIni = fvm.DataInicio;
            var dtFim = fvm.DataFinal;

            if (fvm.DataInicio == null)
            {
                dtIni = new DateTime(DateTime.Now.Year, dtIni.Month, 1, 0, 0, 0);
            }
            else
            {
                dtIni = new DateTime(dtIni.Year, dtIni.Month, dtIni.Day, 0, 0, 0);
            }

            if (fvm.DataFinal == null)
            {
                dtFim = DateTime.Now;
            }
            else
            {
                dtFim = new DateTime(dtFim.Year, dtFim.Month, dtFim.Day, 23, 59, 59);
            }

            var dados = _vendasService.GetGraficoVendasPorPeriodo(dtIni, dtFim);
            if (dados == null)
            {
                return null;
            }
            else
            {
                string json = JsonConvert.SerializeObject(dados, Formatting.Indented);
                return Json(json);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GraficoProdutoMaisVendido([FromBody] FiltroRelatorioView fvm)
        {
            var dtIni = fvm.DataInicio;
            var dtFim = fvm.DataFinal;

            if (fvm.DataInicio == null)
            {
                dtIni = new DateTime(DateTime.Now.Year, dtIni.Month, 1, 0, 0, 0);
            }
            else
            {
                dtIni = new DateTime(dtIni.Year, dtIni.Month, dtIni.Day, 0, 0, 0);
            }

            if (fvm.DataFinal == null)
            {
                dtFim = DateTime.Now;
            }
            else
            {
                dtFim = new DateTime(dtFim.Year, dtFim.Month, dtFim.Day, 23, 59, 59);
            }

            var dados = _vendasService.GetGraficoTop5ProdutosVendidos(dtIni, dtFim);
            if (dados == null)
            {
                return null;
            }
            else
            {
                string json = JsonConvert.SerializeObject(dados, Formatting.Indented);
                return Json(json);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GraficoQuantidadeVendasUsuarios([FromBody] FiltroRelatorioView fvm)
        {
            var dtIni = fvm.DataInicio;
            var dtFim = fvm.DataFinal;

            if (fvm.DataInicio == null)
            {
                dtIni = new DateTime(DateTime.Now.Year, dtIni.Month, 1, 0, 0, 0);
            }
            else
            {
                dtIni = new DateTime(dtIni.Year, dtIni.Month, dtIni.Day, 0, 0, 0);
            }

            if (fvm.DataFinal == null)
            {
                dtFim = DateTime.Now;
            }
            else
            {
                dtFim = new DateTime(dtFim.Year, dtFim.Month, dtFim.Day, 23, 59, 59);
            }

            var dados = _vendasService.GetGraficoQuantidadeVendasUsuarios(dtIni, dtFim);
            if (dados == null)
            {
                return null;
            }
            else
            {
                string json = JsonConvert.SerializeObject(dados, Formatting.Indented);
                return Json(json);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GraficoCategoriasMaisVendida([FromBody] FiltroRelatorioView fvm)
        {
            var dtIni = fvm.DataInicio;
            var dtFim = fvm.DataFinal;

            if (fvm.DataInicio == null)
            {
                dtIni = new DateTime(DateTime.Now.Year, dtIni.Month, 1, 0, 0, 0);
            }
            else
            {
                dtIni = new DateTime(dtIni.Year, dtIni.Month, dtIni.Day, 0, 0, 0);
            }

            if (fvm.DataFinal == null)
            {
                dtFim = DateTime.Now;
            }
            else
            {
                dtFim = new DateTime(dtFim.Year, dtFim.Month, dtFim.Day, 23, 59, 59);
            }

            var dados = _vendasService.GetGraficoTop5ProdutosVendidosPorCategoria(dtIni, dtFim);
            if (dados == null)
            {
                return null;
            }
            else
            {
                string json = JsonConvert.SerializeObject(dados, Formatting.Indented);
                return Json(json);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GraficoTotalPorFormaPag([FromBody] FiltroRelatorioView fvm)
        {
            var dtIni = fvm.DataInicio;
            var dtFim = fvm.DataFinal;

            if (fvm.DataInicio == null)
            {
                dtIni = new DateTime(DateTime.Now.Year, dtIni.Month, 1, 0, 0, 0);
            }
            else
            {
                dtIni = new DateTime(dtIni.Year, dtIni.Month, dtIni.Day, 0, 0, 0);
            }

            if (fvm.DataFinal == null)
            {
                dtFim = DateTime.Now;
            }
            else
            {
                dtFim = new DateTime(dtFim.Year, dtFim.Month, dtFim.Day, 23, 59, 59);
            }

            var dados = _vendasService.GetGraficoPorFormaPag(dtIni, dtFim);
            if (dados == null)
            {
                return null;
            }
            else
            {
                string json = JsonConvert.SerializeObject(dados, Formatting.Indented);
                return Json(json);
            }
        }

        [HttpGet]
        public IActionResult Error(string message, int codigoErro)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Codigo = codigoErro };
            return View(viewModel);
        }

    };
}
