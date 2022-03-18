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
    public class ProdutoService
    {
        private readonly AlphaMarketPDVContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public ProdutoService(AlphaMarketPDVContext context, IHostingEnvironment appEnvironment) 
        {
            this._context = context;
            this._appEnvironment = appEnvironment;
        }

        public async Task<List<Produto>> GetProdutosAsync()
        {
            return await _context.Produto.OrderBy(x => x.DescricaoLonga).ToListAsync();
        }

        public bool GetVerificarCodigoExistente(Produto produto)
        {
            var qtd = _context.Produto.Where(p => p.Codigo == produto.Codigo).Count();

            if (qtd > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Produto> GetProdutoPorIdAsync(int id) 
        {
            return await _context.Produto.Include(obj => obj.Categoria).Include(obj => obj.UnidadeMedida).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<Produto> GetProdutoPorCodigoAsync(string codigo) 
        {
            return await _context.Produto.FirstOrDefaultAsync(p => p.Codigo == codigo);    
        }

        public async Task<Produto> GetProdutoPorIdNoTrackingAsync(int id)
        {
            return await _context.Produto.AsNoTracking().Include(obj => obj.Categoria).Include(obj => obj.UnidadeMedida).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public string UploadImagemProduto(Produto produto)
        {
            string pastaFotos = Path.Combine(_appEnvironment.WebRootPath, "images\\produtos");
            string nomeUnicoArquivo = Guid.NewGuid().ToString() + "." + Path.GetExtension(produto.FotoProduto.FileName);
            string caminhoArquivo = Path.Combine(pastaFotos, nomeUnicoArquivo);
            using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                produto.FotoProduto.CopyTo(fileStream);
            }
            return nomeUnicoArquivo;
        }

        public void ExcluirImagemProduto(Produto produto) 
        {
            if ((produto.Foto != null) && (produto.Foto != ""))
            {
                string pastaFotos = Path.Combine(_appEnvironment.WebRootPath, "images\\produtos");
                string nomeArquivo = produto.Foto;
                string caminhoArquivo = Path.Combine(pastaFotos, nomeArquivo);
                File.Delete(caminhoArquivo);  
            }
        }

        public async Task InserirAsync(Produto obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            try
            {
                var obj = await _context.Produto.FindAsync(id);
                ExcluirImagemProduto(obj);
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
                //obj.Estoque = await _context.Estoque.FirstAsync();
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
