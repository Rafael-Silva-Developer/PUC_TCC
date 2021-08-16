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

        public async Task<List<Produto>> ListarTodosAsync()
        {
            return await _context.Produto.OrderBy(x => x.DescricaoLonga).ToListAsync();
        }

        public async Task InserirAsync(Produto obj)
        {
            //obj.Estoque = _context.Estoque.First();
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Produto> ListarPorIdAsync(int id) 
        {
            return await _context.Produto.Include(obj => obj.Categoria).Include(obj => obj.UnidadeMedida).FirstOrDefaultAsync(obj => obj.Id == id);
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

        public string UploadImagemProduto(Produto produto)
        {
            string nomeUnicoArquivo = null;

            if (produto.FotoProduto != null)
            {
                string pastaFotos = Path.Combine(_appEnvironment.WebRootPath, "images\\produtos");
                nomeUnicoArquivo = Guid.NewGuid().ToString() + "." + Path.GetExtension(produto.FotoProduto.FileName);
                string caminhoArquivo = Path.Combine(pastaFotos, nomeUnicoArquivo);
                using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    produto.FotoProduto.CopyTo(fileStream);
                }
            }
            return nomeUnicoArquivo;
        }

        public void ExcluirImagemProduto(Produto produto) 
        {
            if ((produto.FotoAux != produto.Foto) && 
                (produto.FotoAux != null) && 
                (produto.FotoAux != "") &&
                (produto.FotoProduto != null))
            {
                string pastaFotos = Path.Combine(_appEnvironment.WebRootPath, "images\\produtos");
                string nomeArquivo = produto.FotoAux;
                string caminhoArquivo = Path.Combine(pastaFotos, nomeArquivo);
                File.Delete(caminhoArquivo);  
            }

            if ((produto.FotoAux == null && produto.FotoProduto == null && 
                 produto.Foto != null && produto.Foto != "")) 
            {
                string pastaFotos = Path.Combine(_appEnvironment.WebRootPath, "images\\produtos");
                string nomeArquivo = produto.Foto;
                string caminhoArquivo = Path.Combine(pastaFotos, nomeArquivo);
                File.Delete(caminhoArquivo);
            }
        }
    }
}
