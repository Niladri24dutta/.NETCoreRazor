using ShoppingCartApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartApp.ViewModel
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartModel ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}
