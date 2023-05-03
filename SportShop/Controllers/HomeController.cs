using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportShop.Interface;
using SportShop.Models;
using SportShop.ViewModels.Category;
using System.Diagnostics;

namespace SportShop.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IHomeService _homeService;
        public HomeController(ILogger<HomeController> logger, IProductService productService,ICategoryService categoryService, IHomeService homeService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.categories = await  _categoryService.GetAll();
            
            return View(await _homeService.HomePage());
        }

        public async Task<IActionResult> ProductDetail(int? id)
        {
            return View(await _productService.GetById(id));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> ContactAsync()
        {
            ViewBag.categories = await _categoryService.GetAll();
            return View();
        }
    }
}