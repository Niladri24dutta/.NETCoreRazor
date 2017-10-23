using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.Models;
using ShoppingCartApp.ViewModel;

namespace ShoppingCartApp.Controllers
{
    public class PieController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPieRepository _pieRepository;

        public PieController(ICategoryRepository categoryRepository,IPieRepository pieRepository)
        {
            this._categoryRepository = categoryRepository;
            this._pieRepository = pieRepository;
        }
        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string currentCategory = string.Empty;
            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.Pies.OrderBy(x => x.PieId);
                currentCategory = "All Pies";
            }
            else
            {
                pies = _pieRepository.Pies.Where(p => p.Category.CategoryName == category)
                   .OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.Categories.FirstOrDefault(c => c.CategoryName == category).CategoryName;
            }
            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            });
        }

        public IActionResult Details(int id)
        {
            var existingPie = _pieRepository.GetPieById(id);
            if(existingPie == null)
            {
                return NotFound();
            }
            return View(existingPie);
        }
    }
}
