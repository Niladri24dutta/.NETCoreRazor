using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCartApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartApp.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryMenu(ICategoryRepository repository)
        {
            _categoryRepository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.Categories.OrderBy(x => x.CategoryName);
            return View(categories);
        }
    }
}
