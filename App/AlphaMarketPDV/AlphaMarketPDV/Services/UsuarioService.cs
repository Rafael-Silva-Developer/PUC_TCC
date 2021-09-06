using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Data;
using AlphaMarketPDV.Models;
using AlphaMarketPDV.Models.Enums;
using Microsoft.EntityFrameworkCore;
using AlphaMarketPDV.Services.Exceptions;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AlphaMarketPDV.Services
{
    public class UsuarioService
    {
        private readonly AlphaMarketPDVContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public UsuarioService(AlphaMarketPDVContext context, IHostingEnvironment appEnvironment) 
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<List<Usuario>> ListarTodosAsync()
        {
            return await _context.Usuario.OrderBy(u => u.Login).ToListAsync();
        }

        public async Task InserirAsync(Usuario usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> ListarPorIdAsync(int id)
        {
            return await _context.Usuario.Include(usuario => usuario.Loja).FirstOrDefaultAsync(usuario => usuario.Id == id);
        }

        public async Task<Usuario> ListarPorIdNoTrackingAsync(int id)
        {
            return await _context.Usuario.AsNoTracking().Include(usuario => usuario.Loja).FirstOrDefaultAsync(usuario => usuario.Id == id);
        }

        public async Task RemoverAsync(int id)
        {
            try
            {
                var usuario = await _context.Usuario.FindAsync(id);
                ExcluirImagemUsuario(usuario);
                _context.Usuario.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possível remover este usuário, pois há históricos de operações!");
            }
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            var existeNaBase = await _context.Usuario.AnyAsync(x => x.Id == usuario.Id);

            if (!existeNaBase)
            {
                throw new NotFoundException("Usuário não encontrado para atualização!");
            }

            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public bool LoginExistente(Usuario usuario)
        {
            var qtd = _context.Usuario.Where(x => x.Login == usuario.Login).Count();

            if (qtd > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string UploadImagemUsuario(Usuario usuario)
        {
            string pastaFotos = Path.Combine(_appEnvironment.WebRootPath, "images\\usuarios");
            string nomeUnicoArquivo = Guid.NewGuid().ToString() + "." + Path.GetExtension(usuario.FotoUsuarioLoad.FileName);
            string caminhoArquivo = Path.Combine(pastaFotos, nomeUnicoArquivo);
            using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                usuario.FotoUsuarioLoad.CopyTo(fileStream);
            }
            return nomeUnicoArquivo;
        }

        public void ExcluirImagemUsuario(Usuario usuario)
        {
            if ((usuario.FotoUsuario != null) && (usuario.FotoUsuario != ""))
            {
                string pastaFotos = Path.Combine(_appEnvironment.WebRootPath, "images\\usuarios");
                string nomeArquivo = usuario.FotoUsuario;
                string caminhoArquivo = Path.Combine(pastaFotos, nomeArquivo);
                File.Delete(caminhoArquivo);
            }
        }
    }
}
