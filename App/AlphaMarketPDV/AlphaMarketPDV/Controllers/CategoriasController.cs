using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaMarketPDV.Services;
using AlphaMarketPDV.Models;

namespace AlphaMarketPDV.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CategoriaService _categoriaService;

        public CategoriasController(CategoriaService categoriaService) 
        {
            this._categoriaService = categoriaService;
        }

        public IActionResult Index()
        {
            var list = _categoriaService.ListarTodos();
            return View(list);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria) 
        {
            _categoriaService.Inserir(categoria);
            return RedirectToAction(nameof(Index));
        }
    }
}
