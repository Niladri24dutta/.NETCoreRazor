using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.Models;
using ShoppingCartApp.ViewModel;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;
        public HomeController(IPieRepository repository)
        {
            _pieRepository = repository;
        }
        public ViewResult Index()
        {
            var model = new HomeViewModel
            {
                PiesOfTheWeek = _pieRepository.PiesOfTheWeek
            };
            return View(model);
        }
    }
}
