using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartApp.Models
{
    public class ShoppingCartModel
    {
        private readonly AppDbContext _appDbContext;
        public ShoppingCartModel(AppDbContext context)
        {
            _appDbContext = context;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCartModel GetCart(IServiceProvider services)
        {
            ISession currentSession = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string cartId = currentSession.GetString("CartId") ?? Guid.NewGuid().ToString();
            currentSession.SetString("CartId", cartId);
            return new ShoppingCartModel(context) { ShoppingCartId = cartId }; 
        }

        public void AddToCart(Pie pie,int amount)
        {
            var shoppingCarttem = _appDbContext.ShoppingCartItems.FirstOrDefault(s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);
            if(shoppingCarttem == null)
            {
                shoppingCarttem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };

                _appDbContext.ShoppingCartItems.Add(shoppingCarttem);
            }
            else
            {
                shoppingCarttem.Amount++;
            }
            _appDbContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _appDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Pie)
                           .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _appDbContext.SaveChanges();
        }



        public decimal GetShoppingCartTotal()
        {
            var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }

    }
}
