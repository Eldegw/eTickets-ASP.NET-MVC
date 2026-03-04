using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrderByUserIdAsync(string userId)
        {
           var orders = await _context.Orders.Include(x=>x.OrderItems).ThenInclude(x=>x.Movie).Where(n=>n.UserId == userId).ToListAsync();
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
          var order = new Order()
          {
              UserId = userId,
              Email = userEmailAddress
          };
           await _context.Orders.AddAsync(order);
           await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderitem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };
                await _context.OrderItems.AddAsync(orderitem);

            }

            await _context.SaveChangesAsync();



        }


    }
}
