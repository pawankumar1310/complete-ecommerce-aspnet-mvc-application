using eTickets.Data.Cart;
using eTickets.Data.MovieModels;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
    {
    public class OrderController : Controller
        {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;

        public OrderController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrderService orderService, IConfiguration configuration)
            {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
            _configuration = configuration;
            }
        public IActionResult ShoppingCart()
            {
            ViewBag.SecretKey = _configuration["AppSettings:SecretKey"];
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
                {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
                };
            return View(response);
            }
        public async Task<RedirectToActionResult> AddItemToShoppingCart(int id)
            {
            
            var item = await _moviesService.GetMovieByIdAsync(id);
            if(item != null)
                {
                _shoppingCart.AddItemToCart(item);
                }
            return RedirectToAction(nameof(ShoppingCart));
            }
        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart(int id)
            {
            var item = await _moviesService.GetMovieByIdAsync(id);
            if (item != null)
                {
                _shoppingCart.RemoveItemFromCart(item);
                }
            return RedirectToAction(nameof(ShoppingCart));
            }
        public async Task<IActionResult> CompleteOrder()
            {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmailAddress = "";
            await _orderService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
            }
        public async Task<IActionResult> Index()
            {
            string userId = "";
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return View(orders);
            }
        }
    }
