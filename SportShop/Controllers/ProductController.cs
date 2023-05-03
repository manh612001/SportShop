using Microsoft.AspNetCore.Mvc;
using SportShop.Interface;
using SportShop.Service;

namespace SportShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            ViewBag.categories = await _categoryService.GetAll();
            return View(await _productService.GetById(id));
        }
        public async Task<IActionResult> ProductsByCategory(int? id)
        {
            ViewBag.categories = await _categoryService.GetAll();
            return View(await _productService.GetProductsByCategory(id));
        }
        
    }
}
