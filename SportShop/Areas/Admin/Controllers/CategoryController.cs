using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportShop.Interface;
using SportShop.Models;
using SportShop.ViewModels.Category;

namespace SportShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAll());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryViewModel model)
        {
            await _categoryService.Add(model);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return View(await _categoryService.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            await _categoryService.Update(model);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _categoryService.GetById(id));

        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            await _categoryService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
