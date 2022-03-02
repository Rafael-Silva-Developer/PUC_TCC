using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Data;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AlphaMarketPDV.Services
{
    public class ContatoService
    {
        private readonly AlphaMarketPDVContext _context;

        public ContatoService(AlphaMarketPDVContext context) 
        {
            _context = context;
        }

        public async Task<List<Contato>> ListarTodosContatosAsync()
        {
            return await _context.Contato.OrderBy(obj => obj.Id).ToListAsync();
        }

        public async Task<List<Contato>> ListarTodosContatosFornecedorIdAsync(int idFornecedor) 
        {
            return await _context.Contato.Where(c => c.FornecedorId == idFornecedor).ToListAsync();   
        }

        public async Task<Contato> ListarContatoPorIdAsync(int id)
        {
            return await _context.Contato.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task InserirContatoAsync(Contato obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Contato> ListarContatoPorIdNoTrackingAsync(int id)
        {
            return await _context.Contato.AsNoTracking().FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoverContatoAsync(int id)
        {
            try
            {
                var obj = await _context.Contato.FindAsync(id);
                _context.Contato.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possível remover o contato, pois há histórico de operações!");
            }
        }

        public async Task RemoverContatosFornecedorAsync(int id) 
        {
            try 
            {
                var lstContatos = await ListarTodosContatosFornecedorIdAsync(id);
                foreach (var i in lstContatos) 
                {
                    _context.Contato.Remove(i);
                    await _context.SaveChangesAsync();
                }
            } 
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possível remover os contatos desse fornecedor.");
            }
        }

        public async Task AtualizarContatoAsync(Contato oContato)
        {
            var existeNaBase = await _context.Contato.AnyAsync(obj => obj.Id == oContato.Id);

            if (!existeNaBase)
            {
                throw new NotFoundException("Contato não encontrado para atualização!");
            }

            try
            {
                _context.Update(oContato);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
