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
    public class FluxoCaixaService
    {
        private readonly AlphaMarketPDVContext _context;

        public FluxoCaixaService(AlphaMarketPDVContext context)
        {
            _context = context;
        }

        public async Task InserirCaixaAsync(Caixa oCaixa)
        {
            _context.Add(oCaixa);
            await _context.SaveChangesAsync();
        }

        public async Task InserirCaixaPagamentoAsync(CaixaPagamento oCaixaPag)
        {
            _context.Add(oCaixaPag);
            await _context.SaveChangesAsync();
        }

        private async Task<int> GetUltimaAberturaAsync()
        {
            try
            {
                return await _context.Caixa.Where(c => c.TipoOperacao == TipoCaixa.ABERTURA).Select(c => c.Id).MaxAsync();
            }
            catch
            {
                return 0;
            }           
        }

        private async Task<int> GetUltimoFechamentoAsync()
        {
            try
            {
                return await _context.Caixa.Where(c => c.TipoOperacao == TipoCaixa.FECHAMENTO).Select(c => c.Id).MaxAsync();
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> VerificarCaixaAbertoAsync()
        {
            var IdAbertura = await GetUltimaAberturaAsync();
            var IdFechamento = await GetUltimoFechamentoAsync();

            return (IdAbertura > IdFechamento);
        }

        public async Task<bool> VerificarCaixaFechadoAsync()
        {
            var IdAbertura = await GetUltimaAberturaAsync();
            var IdFechamento = await GetUltimoFechamentoAsync();

            return (IdFechamento > IdAbertura);
        }

        public async Task<Caixa> GetCaixaPorIdentificadorAsync(string id)
        {
            return await _context.Caixa.FirstOrDefaultAsync(obj => obj.IdentificadorRegistro == id);
        }



    }
}
