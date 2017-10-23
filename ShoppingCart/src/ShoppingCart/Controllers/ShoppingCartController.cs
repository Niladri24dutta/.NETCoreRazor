using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.Models;
using ShoppingCartApp.ViewModel;
using System.Diagnostics;

namespace ShoppingCartApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ShoppingCartModel _shoppingCart;

        public ShoppingCartController(IPieRepository repository,ShoppingCartModel model)
        {
            _pieRepository = repository;
            _shoppingCart = model;
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            //Debug.WriteLine(TempData.Keys);           
            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            var existingPies = _pieRepository.Pies.FirstOrDefault(x => x.PieId == pieId);
            if(existingPies != null)
            {
                _shoppingCart.AddToCart(existingPies, 1);
                TempData["ItemAdded"] = "Success";
            }
            return RedirectToAction("Index");
        }
    }
}
