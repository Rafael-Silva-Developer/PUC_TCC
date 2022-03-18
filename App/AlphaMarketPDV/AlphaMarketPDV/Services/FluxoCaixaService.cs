using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Data;
using AlphaMarketPDV.Models;
using Microsoft.EntityFrameworkCore;
using AlphaMarketPDV.Models.Enums;
using System;
using System.Collections.Generic;

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

        public async Task<List<Caixa>> GetOperacaoesCaixaPorPeriodoAsync(DateTime? dataIni, DateTime? dataFim)
        {
            var result = from obj in _context.Caixa select obj;

            if (dataIni.HasValue)
            {
                result = result.Where(x => x.DataHora >= dataIni.Value);
            };

            if (dataFim.HasValue)
            {
                result = result.Where(x => x.DataHora <= dataFim.Value);
            };

            return await result
                .Include(x => x.Usuario)
                .OrderBy(x => x.DataHora)
                .ToListAsync();
        }

        public async Task<List<CaixaPagamento>> GetItensCaixaPorIdCaixaAsync(int idCaixa)
        {         
            return await _context.CaixaPagamento.Include(obj => obj.FormaPagamento).Where(obj => obj.CaixaId == idCaixa).ToListAsync();         
        }

    }
}
