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
        public HomeController(ILogger<HomeController> logger, IProductService productService,ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            List<CategoryViewModel> students = await _categoryService.GetAll();
            
            ViewBag.SelectList = students;
            return View(await _productService.GetAll());
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
    }
}