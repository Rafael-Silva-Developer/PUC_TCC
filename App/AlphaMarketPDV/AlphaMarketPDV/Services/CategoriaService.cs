using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Data;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AlphaMarketPDV.Services
{
    public class CategoriaService
    {
        private readonly AlphaMarketPDVContext _context;

        public CategoriaService(AlphaMarketPDVContext context) 
        {
            _context = context;
        }

        public async Task<List<Categoria>> ListarTodosAsync() 
        {
            return await _context.Categoria.OrderBy(x => x.Descricao).ToListAsync();
        }

        public async Task InserirAsync(Categoria obj) 
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Categoria> ListarPorIdAsync(int id)
        {
            return await _context.Categoria.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<Categoria> ListarPorIdNoTrackingAsync(int id)
        {
            return await _context.Categoria.AsNoTracking().FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoverAsync(int id)
        {
            try
            {
                var obj = await _context.Categoria.FindAsync(id);
                _context.Categoria.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possível remover esta categoria, pois há produtos associados!");
            }
        }

        public async Task UpdateAsync(Categoria obj)
        {
            var existeNaBase = await _context.Categoria.AnyAsync(x => x.Id == obj.Id);

            if (!existeNaBase)
            {
                throw new NotFoundException("Categoria não encontrada para atualização!");
            }

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

        public bool DescricaoCategoriaExistente(Categoria categoria)
        {
            var qtd = _context.Categoria.Where(c => c.Descricao == categoria.Descricao).Count();

            if (qtd > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
