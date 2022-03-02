using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Data;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using AlphaMarketPDV.Models.Enums;

namespace AlphaMarketPDV.Services
{
    public class EstoqueService
    {
        private readonly AlphaMarketPDVContext _context;
        private readonly ProdutoService _produtoService;
        private readonly LojaService _lojaService;

        public EstoqueService(AlphaMarketPDVContext context, ProdutoService produtoService, LojaService lojaService)
        {
            _context = context;
            _produtoService = produtoService;
            _lojaService = lojaService;
        }

        public async Task<List<Estoque>> GetProdutosEstoqueAsync()
        {
            return await _context.Estoque.Include(obj => obj.Loja).Include(obj => obj.Produto).OrderBy(obj => obj.Produto.DescricaoCurta).ToListAsync();
        }

        public async Task<EntradaEstoque> GetEntradaPorIdentificadorAsync(string id)
        {
            return await _context.EntradaEstoque.FirstOrDefaultAsync(obj => obj.IdentificadorRegistro == id);
        }

        public async Task<SaidaEstoque> GetSaidaPorIdentificadorAsync(string id) 
        {
            return await _context.SaidaEstoque.FirstOrDefaultAsync(obj => obj.IdentificadorRegistro == id);
        }

        public async Task<Estoque> GetEstoqueProdutoAsync(int idProduto)
        {
            return await _context.Estoque.FirstOrDefaultAsync(obj => obj.ProdutoId == idProduto && obj.LojaId == 1);
        }

        public async Task InserirEntradaAsync(EntradaEstoque obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InserirItemEntradaAsync(ItemEntradaEstoque obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InserirSaidaAsync(SaidaEstoque obj) 
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InserirItemSaidaAsync(ItemSaidaEstoque obj) 
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        private async Task InserirEstoqueAsync(Estoque obj) 
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        private async Task AtualizarEstoqueAsync(Estoque obj) 
        {
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task AtualizarEstoqueAsync(int idProduto, double qtd, string op)
        {
            if ((op != null) && (qtd > 0) && (idProduto > 0))
            {
                var objEstoque = await GetEstoqueProdutoAsync(idProduto);
                var objProduto = await _produtoService.GetProdutoPorIdAsync(idProduto);
                var objLoja = await _lojaService.GetLojaPorIdAsync(1); 

                if (objEstoque != null)
                {                   
                    if (op == "C")
                    {
                        objEstoque.Saldo += qtd;

                        if (objEstoque.Saldo >= objProduto.QuantMinima)
                        {
                            objEstoque.Status = StatusEstoque.NORMAL;
                        }
                        else
                        {
                            objEstoque.Status = StatusEstoque.BAIXO;
                        }
                    }
                    else if (op == "D")
                    {
                        objEstoque.Saldo -= qtd;

                        if (objEstoque.Saldo >= objProduto.QuantMinima)
                        {
                            objEstoque.Status = StatusEstoque.NORMAL;
                        }
                        else
                        {
                            objEstoque.Status = StatusEstoque.BAIXO;
                        }
                    }
                    await AtualizarEstoqueAsync(objEstoque);
                }
                else
                {
                    if (op == "C") 
                    {
                        var newEstoqueProduto = new Estoque { 
                            LojaId = 1,
                            Loja = objLoja,
                            Saldo = qtd,
                            Status = StatusEstoque.BAIXO,
                            Produto = objProduto,
                            ProdutoId = idProduto
                        };

                        if (objProduto.QuantMinima >= qtd)
                        {
                            newEstoqueProduto.Status = StatusEstoque.NORMAL;
                        }
                        else
                        {
                            newEstoqueProduto.Status = StatusEstoque.BAIXO;
                        }

                        await InserirEstoqueAsync(newEstoqueProduto);
                    }
                }
            }
        }
    }
}
