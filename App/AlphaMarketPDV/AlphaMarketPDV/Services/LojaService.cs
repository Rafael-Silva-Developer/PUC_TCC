using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Data;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AlphaMarketPDV.Services
{
    public class LojaService
    {
        private readonly AlphaMarketPDVContext _context;

        public LojaService(AlphaMarketPDVContext context) 
        {
            _context = context;      
        }

        public async Task<List<Loja>> ListarTodosAsync()
        {
            return await _context.Loja.OrderBy(l => l.Descricao).ToListAsync();
        }

        public async Task InserirAsync(Loja loja)
        {
            _context.Add(loja);
            await _context.SaveChangesAsync();
        }

        public async Task<Loja> GetLojaPorIdAsync(int id)
        {
            return await _context.Loja.FirstOrDefaultAsync(loja => loja.Id == id);
        }

        public async Task<Loja> ListarPorIdNoTrackingAsync(int id)
        {
            return await _context.Loja.AsNoTracking().FirstOrDefaultAsync(loja => loja.Id == id);
        }

        public async Task RemoverAsync(int id)
        {
            try
            {
                var loja = await _context.Loja.FindAsync(id);
                _context.Loja.Remove(loja);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possível remover esta loja, pois há histórico de operações!");
            }
        }

        public async Task UpdateAsync(Loja obj)
        {
            var existeNaBase = await _context.Loja.AnyAsync(loja => loja.Id == obj.Id);

            if (!existeNaBase)
            {
                throw new NotFoundException("Loja não encontrada para atualização!");
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

        public bool DescricaoLojaExistente(Loja loja)
        {
            var qtd = _context.Loja.Where(l => l.Descricao == loja.Descricao).Count();

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
