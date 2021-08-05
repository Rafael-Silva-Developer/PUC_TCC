using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Data;
using AlphaMarketPDV.Models;

namespace AlphaMarketPDV.Services
{
    public class CategoriaService
    {
        private readonly AlphaMarketPDVContext _context;

        public CategoriaService(AlphaMarketPDVContext context) 
        {
            _context = context;
        }

        public List<Categoria> ListarTodos() 
        {
            return _context.Categoria.OrderBy(x => x.Descricao).ToList();
        }

        public void Inserir(Categoria obj) 
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
