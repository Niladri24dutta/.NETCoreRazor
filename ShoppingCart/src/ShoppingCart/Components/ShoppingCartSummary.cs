using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.Models;
using ShoppingCartApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartApp.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCartModel _shoppingCart;

        public ShoppingCartSummary(ShoppingCartModel model)
        {
            _shoppingCart = model;
        }

        public IViewComponentResult Invoke()
        {
            //var items = _shoppingCart.GetShoppingCartItems();
            var items = new List<ShoppingCartItem>() { new ShoppingCartItem(), new ShoppingCartItem() };
            _shoppingCart.ShoppingCartItems = items;
            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }
    }
}
