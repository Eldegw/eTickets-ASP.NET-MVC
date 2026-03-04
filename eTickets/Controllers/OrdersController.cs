using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;

        public OrdersController(IMovieService movieService, ShoppingCart shoppingCart, IOrderService orderService)
        {
            _movieService = movieService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

    
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _movieService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _movieService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }


        public async Task<IActionResult> CompleteOrder()
        {
            var item = _shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmailAddress = "";

            await _orderService.StoreOrderAsync(item, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("CompleteOrder", item);

        }

        public async Task<IActionResult> Index()
        {
            string userid = "";

            var orders = await _orderService.GetOrderByUserIdAsync(userid);
            return View("Index",orders);
        }

    }

   
}