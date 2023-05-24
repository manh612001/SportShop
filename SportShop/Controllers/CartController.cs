using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Infrastructure;
using SportShop.Interface;
using SportShop.Models;
using SportShop.ViewModels.Cart;

namespace SportShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly DatabaseDbContext _context;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;

        public CartController(DatabaseDbContext context, ICategoryService categoryService,IAccountService accountService)
        {
            _context = context;
            _categoryService = categoryService;
            _accountService = accountService;
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CartViewModel model)
        {
            List<CartItems> cart = HttpContext.Session.GetJson<List<CartItems>>("Cart") ?? new List<CartItems>();
            
            var userCurrentId = await _accountService.GetByName(User.Identity.Name);
            try
            {

                var newCart = new Cart()
                {
                    grandTotal = cart.Sum(x => x.Quantity * x.Price),
                };
                _context.Carts.Add(newCart);
                foreach(var item in cart) 
                {
                    _context.CartItems.Add(new CartItems()
                    {
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        UserId = userCurrentId.Id,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Image = item.Image,
                    });
                }
                await _context.SaveChangesAsync();
                
                var result = _context.CartItems.OrderByDescending(x => x.Id).Take(cart.Count());
                foreach(var item in result)
                {
                    _context.Orders.Add(new Order()
                    {
                        CartId = newCart.Id,
                        CartItemId = item.Id,
                        
                    });

                }
                await _context.SaveChangesAsync();
                HttpContext.Session.Remove("Cart");
                TempData["alert"] = "Đặt hàng thành công";
                TempData["status"] = "alert-success";
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }

            
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Index()
        {
            string name = User.Identity.Name;
            var userCurrentId = await _accountService.GetByName(name);
            ViewBag.UserId = userCurrentId.Id;
            ViewBag.categories = await _categoryService.GetAll();
            List<CartItems> cart = HttpContext.Session.GetJson<List<CartItems>>("Cart") ?? new List<CartItems>();
            CartViewModel cartVM = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price),
            };
            return View(cartVM);
        }
        public async Task<IActionResult> Add(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            List<CartItems> cart = HttpContext.Session.GetJson<List<CartItems>>("Cart") ?? new List<CartItems>();
            CartItems cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();
            var userCurrentId = await _accountService.GetByName(User.Identity.Name);
            if (cartItem == null)
            {
                cart.Add(new CartItems()
                {
                    ProductId = id,
                    UserId= userCurrentId.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                    Image = product.Image,
                });
            }
            else
            {
                cartItem.Quantity += 1;
            }
            HttpContext.Session.SetJson("Cart", cart);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Decrease(long id)
        {
            List<CartItems> cart = HttpContext.Session.GetJson<List<CartItems>>("Cart");

            CartItems cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(long id)
        {
            List<CartItems> cart = HttpContext.Session.GetJson<List<CartItems>>("Cart");

            cart.RemoveAll(p => p.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }
        
        


    }
}
