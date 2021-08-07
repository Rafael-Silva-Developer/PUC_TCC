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
    public class UnidadeMedidaService
    {
        private readonly AlphaMarketPDVContext _context;

        public UnidadeMedidaService(AlphaMarketPDVContext context)
        {
            _context = context;
        }

        public async Task<List<UnidadeMedida>> ListarTodosAsync()
        {
            return await _context.UnidadeMedida.OrderBy(x => x.Descricao).ToListAsync();
        }

        public async Task InserirAsync(UnidadeMedida obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<UnidadeMedida> ListarPorIdAsync(int id)
        {
            return await _context.UnidadeMedida.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoverAsync(int id)
        {
            try
            {
                var obj = await _context.UnidadeMedida.FindAsync(id);
                _context.UnidadeMedida.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possível remover esta unidade de medida, pois há produtos associados!");
            }
        }

        public async Task UpdateAsync(UnidadeMedida obj)
        {
            var existeNaBase = await _context.UnidadeMedida.AnyAsync(x => x.Id == obj.Id);

            if (!existeNaBase)
            {
                throw new NotFoundException("Unidade de medida não encontrada para atualização!");
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
    }
}
