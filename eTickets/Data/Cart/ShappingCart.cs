using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public AppDbContext _context { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems
                .FirstOrDefault(n=>n.Movie.Id == movie.Id  && n.ShoppingCartId == ShoppingCartId);
          
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1

                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _context.SaveChanges();

        }


        public void RemoveItemFromCart(Movie movie)
        {
           var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n=>n.Movie.Id  == movie.Id && n.ShoppingCartId == ShoppingCartId );

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);

                }

            }
         
            _context.SaveChanges();

        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Include(n => n.Movie).ToList());
        }

        public async Task ClearShoppingCartAsync()
        {
            var item = await _context.ShoppingCartItems.Where(x=>x.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(item);
            await _context.SaveChangesAsync();
        }


        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems
                .Where(x => x.ShoppingCartId == ShoppingCartId)
                .Select(n => n.Movie.Price * n.Amount)
                .Sum();

            return total;
        }

      

        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = service.GetRequiredService<AppDbContext>();

            var cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context)
            {
                ShoppingCartId = cartId,
            };
        }






    }
}