using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Data;
using AlphaMarketPDV.Models;
using Microsoft.EntityFrameworkCore;
using AlphaMarketPDV.Services.Exceptions;

namespace AlphaMarketPDV.Services
{
    public class ProdutoService
    {
        private readonly AlphaMarketPDVContext _context;

        public ProdutoService(AlphaMarketPDVContext context) 
        {
            this._context = context;
        }

        public async Task<List<Produto>> ListarTodosAsync()
        {
            return await _context.Produto.OrderBy(x => x.DescricaoLonga).ToListAsync();
        }

        public async Task InserirAsync(Produto obj)
        {
            obj.Estoque = _context.Estoque.First();
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Produto> ListarPorIdAsync(int id) 
        {
            return await _context.Produto.Include(obj => obj.Categoria).Include(obj => obj.UnidadeMedida).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoverAsync(int id) 
        {
            try
            {
                var obj = await _context.Produto.FindAsync(id);
                _context.Produto.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possível remover um produto com histórico de operações!");
            }
        }

        public async Task UpdateAsync(Produto obj) 
        {
            var existeNaBase = await _context.Produto.AnyAsync(x => x.Id == obj.Id);

            if (!existeNaBase)
            {
                throw new NotFoundException("Produto não encontrado para atualização!");
            }

            try
            {
                obj.Estoque = await _context.Estoque.FirstAsync();
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
