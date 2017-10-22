using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;


namespace ShoppingCart.Controllers
{
    public class PieController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPieReposiory _pieRepository;

        public PieController(ICategoryRepository categoryRepository,IPieReposiory pieRepository)
        {
            this._categoryRepository = categoryRepository;
            this._pieRepository = pieRepository;
        }
        public ViewResult Index()
        {
            return View(this._pieRepository.Pies);
        }
    }
}
