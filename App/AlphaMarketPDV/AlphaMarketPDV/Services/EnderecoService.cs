using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Data;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AlphaMarketPDV.Services
{
    public class EnderecoService
    {
        private readonly AlphaMarketPDVContext _context;

        public EnderecoService(AlphaMarketPDVContext context)
        {
            _context = context;
        }

        public async Task<List<Endereco>> ListarTodosEnderecosAsync()
        {
            return await _context.Endereco.OrderBy(obj => obj.Cidade).ToListAsync();
        }

        public async Task<Endereco> ListarEnderecoPorIdAsync(int id)
        {
            return await _context.Endereco.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<Endereco> ListarEnderecoPorCepAsync(string cep) 
        {
            return await _context.Endereco.FirstOrDefaultAsync(obj => obj.Cep == cep);        
        }

        public async Task InserirEnderecoAsync(Endereco obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Endereco> ListarEnderecoPorIdNoTrackingAsync(int id)
        {
            return await _context.Endereco.AsNoTracking().FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoverEnderecoAsync(int id)
        {
            try
            {
                var obj = await _context.Endereco.FindAsync(id);
                _context.Endereco.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possível remover este endereço, pois há histórico de operações!");
            }
        }

        public async Task AtualizarEnderecoAsync(Endereco oEndereco)
        {
            var existeNaBase = await _context.Endereco.AnyAsync(obj => obj.Id == oEndereco.Id);

            if (!existeNaBase)
            {
                throw new NotFoundException("Endereço não encontrado para atualização!");
            }

            try
            {
                _context.Update(oEndereco);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public bool EnderecoJahExistente(Endereco oEndereco)
        {
            var qtd = _context.Endereco.Where(obj => obj.Cep == oEndereco.Cep).Count();

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
