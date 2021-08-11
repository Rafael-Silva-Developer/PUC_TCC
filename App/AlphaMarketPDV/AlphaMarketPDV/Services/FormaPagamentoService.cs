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
    public class FormaPagamentoService
    {
        private readonly AlphaMarketPDVContext _context;

        public FormaPagamentoService(AlphaMarketPDVContext context) 
        {
            _context = context;
        }

        public async Task<List<FormaPagamento>> ListarTodosAsync()
        {
            return await _context.FormaPagamento.OrderBy(x => x.Descricao).ToListAsync();
        }

        public async Task InserirAsync(FormaPagamento obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<FormaPagamento> ListarPorIdAsync(int id)
        {
            return await _context.FormaPagamento.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoverAsync(int id)
        {
            try
            {
                var obj = await _context.FormaPagamento.FindAsync(id);
                _context.FormaPagamento.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possível remover esta forma de pagamento, pois há histórico de associações!");
            }
        }

        public async Task UpdateAsync(FormaPagamento obj)
        {
            var existeNaBase = await _context.FormaPagamento.AnyAsync(x => x.Id == obj.Id);

            if (!existeNaBase)
            {
                throw new NotFoundException("Forma de pagamento não encontrada para atualização!");
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
