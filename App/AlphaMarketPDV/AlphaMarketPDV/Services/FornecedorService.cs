using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Data;
using AlphaMarketPDV.Models;
using Microsoft.EntityFrameworkCore;
using AlphaMarketPDV.Services.Exceptions;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AlphaMarketPDV.Services
{
    public class FornecedorService
    {
        private readonly AlphaMarketPDVContext _context;

        public FornecedorService(AlphaMarketPDVContext context)
        {
            _context = context;   
        }

        public async Task<List<Fornecedor>> ListarTodosFornecedoresAsync()
        {
            return await _context.Fornecedor.OrderBy(f => f.NomeFantasia).ToListAsync();
        }

        public async Task<Fornecedor> ListarFornecedorPorIdAsync(int id)
        {
            return await _context.Fornecedor.Include(f => f.Endereco).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<Contato>> ListarContatosFornecedorAsync(int id) 
        {
            return await _context.Contato.Where(c => c.FornecedorId == id).ToListAsync();
        }

        public async Task<Fornecedor> ListarFornecedorPorIdNoTrackingAsync(int id)
        {
            return await _context.Fornecedor.AsNoTracking().Include(f => f.Endereco).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task RemoverFornecedorAsync(int id)
        {
            try
            {
                var fornecedor = await _context.Fornecedor.FindAsync(id);
                _context.Fornecedor.Remove(fornecedor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possível remover um fornecedor com histórico de operações!");
            }
        }

        public async Task AtualizarFornecedorAsync(Fornecedor oFornecedor)
        {
            var existeNaBase = await _context.Fornecedor.AnyAsync(x => x.Id == oFornecedor.Id);

            if (!existeNaBase)
            {
                throw new NotFoundException("Fornecedor não encontrado para atualização!");
            }

            try
            {
                _context.Update(oFornecedor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public bool RazaoSocialFornecedorJahExistente(Fornecedor oFornecedor)
        {
            var qtd = _context.Fornecedor.Where(x => x.RazaoSocial == oFornecedor.RazaoSocial).Count();

            if (qtd > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task InserirFornecedorAsync(Fornecedor oFornecedor)
        {
            _context.Add(oFornecedor);
            await _context.SaveChangesAsync();
        }

        
    }
}
