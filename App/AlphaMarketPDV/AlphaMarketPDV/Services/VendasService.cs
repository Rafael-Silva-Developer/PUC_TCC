using System.Threading.Tasks;
using AlphaMarketPDV.Data;
using AlphaMarketPDV.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;

namespace AlphaMarketPDV.Services
{
    public class VendasService
    {
        private readonly AlphaMarketPDVContext _context;

        public VendasService(AlphaMarketPDVContext context)
        {
            _context = context;
        }

        public async Task InserirVendaAsync(Venda obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InserirItemVendaAsync(ItemVenda obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Venda> GetVendaPorIdentificadorAsync(string id)
        {
            return await _context.Venda.FirstOrDefaultAsync(obj => obj.IdentificadorRegistro == id);
        }

        public async Task<List<Venda>> GetVendasPorPeriodoAsync(DateTime? dataIni, DateTime? dataFim)
        {
            var result = from obj in _context.Venda select obj;

            if (dataIni.HasValue)
            {
                result = result.Where(x => x.DataHora >= dataIni.Value);
            };

            if (dataFim.HasValue)
            {
                result = result.Where(x => x.DataHora <= dataFim.Value);
            };

            return await result
                .OrderBy(x => x.DataHora)
                .ToListAsync();
        }

        public async Task<List<ItemVenda>> GetItensVendidoPorIdVendaAsync(int idVenda)
        {
            return await _context.ItemVenda.Include(obj => obj.Produto).Where(obj => obj.VendaId == idVenda).ToListAsync();
        }

        public IQueryable<Object> GetGraficoVendasPorPeriodo(DateTime dtIni, DateTime dtFim)
        {
            return _context.Venda
                .Where(v => v.DataHora >= dtIni && v.DataHora <= dtFim)
                .GroupBy(v => v.DataVenda)
                .Select(v => new { 
                    DataVenda = v.Key, 
                    TotalVenda = v.Sum(x => x.TotalPagar) 
                });
        }

        public IQueryable<Object> GetGraficoTop5ProdutosVendidos(DateTime dtIni, DateTime dtFim)
        {
            return _context.Venda
                        .Join(_context.ItemVenda, v => v.Id, i => i.VendaId, (v, i) => new { Venda = v, ItemVenda = i })
                        .Join(_context.Produto, i => i.ItemVenda.Id, p => p.Id, (i, p) => new { ItemVenda = i, Produto = p })
                        .Where(v => v.ItemVenda.Venda.DataHora >= dtIni && v.ItemVenda.Venda.DataHora <= dtFim)
                        .GroupBy(p => p.Produto.DescricaoCurta)
                        .OrderByDescending(x => x.Sum(i => i.ItemVenda.ItemVenda.Qtd))
                        .Select(v => new {
                                Produto = v.Key,
                                Quantidade = v.Sum(i => i.ItemVenda.ItemVenda.Qtd)
                        }).Take(5);
        }

        public IQueryable<Object> GetGraficoQuantidadeVendasUsuarios(DateTime dtIni, DateTime dtFim)
        {
            return _context.Venda
                        .Join(_context.Caixa, v => v.CaixaId, c => c.Id, (v, c) => new { Venda = v, Caixa = c })
                        .Join(_context.Users, c => c.Caixa.UsuarioId, u => u.Id, (c, u) => new { Caixa = c, UsuarioApp = u })
                        .Where(v => v.Caixa.Venda.DataHora >= dtIni && v.Caixa.Venda.DataHora <= dtFim)
                        .GroupBy(u => u.UsuarioApp.Nome)
                        .Select(u => new { 
                            Usuario = u.Key,
                            Quantidade = u.Count()
                        });
        }

        public IQueryable<Object> GetGraficoTop5ProdutosVendidosPorCategoria(DateTime dtIni, DateTime dtFim)
        {
            return _context.Venda
                        .Join(_context.ItemVenda, v => v.Id, i => i.VendaId, (v, i) => new { Venda = v, ItemVenda = i })
                        .Join(_context.Produto, i => i.ItemVenda.Id, p => p.Id, (i, p) => new { ItemVenda = i, Produto = p })
                        .Join(_context.Categoria, p => p.Produto.CategoriaId, c => c.Id, (p, c) => new { Produto = p, Categoria = c })
                        .Where(v => v.Produto.ItemVenda.Venda.DataHora >= dtIni && v.Produto.ItemVenda.Venda.DataHora <= dtFim)
                        .GroupBy(c => c.Categoria.Descricao)
                        .OrderByDescending(x => x.Count())
                        .Select(c => new {
                            Categoria = c.Key,
                            Quantidade = c.Count()
                        }).Take(5);
        }

        public IQueryable<Object> GetGraficoPorFormaPag(DateTime dtIni, DateTime dtFim)
        {
            return _context.Venda
                        .Join(_context.Caixa, v => v.CaixaId, c => c.Id, (v, c) => new { Venda = v, Caixa = c })
                        .Join(_context.CaixaPagamento, c => c.Caixa.Id, cp => cp.CaixaId, (c, cp) => new { Caixa = c, CaixaPagamento = cp })
                        .Join(_context.FormaPagamento, cp => cp.CaixaPagamento.FormaPagamentoId, fp => fp.Id, (cp, fp) => new { CaixaPagamento = cp, FormaPagamento = fp })
                        .Where(v => v.CaixaPagamento.Caixa.Venda.DataHora >= dtIni && v.CaixaPagamento.Caixa.Venda.DataHora <= dtFim)
                        .GroupBy(fp => fp.FormaPagamento.Descricao)
                        .OrderByDescending(x => x.Sum(cp => cp.CaixaPagamento.CaixaPagamento.ValorPago))
                        .Select(fp => new {
                            FormaPag = fp.Key,
                            Total = fp.Sum(cp => cp.CaixaPagamento.CaixaPagamento.ValorPago)
                        });
        }

    }
}
