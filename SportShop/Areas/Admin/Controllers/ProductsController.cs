using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportShop.Interface;
using SportShop.Models;
using SportShop.ViewModels.Category;
using SportShop.ViewModels.Product;

namespace SportShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productServive;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productServive = productService;
            _categoryService = categoryService;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {

            return View(await _productServive.GetAll());
        }
        //GET: Admin/Products/Create
        public async Task<IActionResult> CreateAsync()
        {
            List<CategoryViewModel> students = await _categoryService.GetAll();
            SelectList selectLists = new SelectList(students, "Id", "Name");
            ViewBag.SelectList = selectLists;
            return View();

        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {

            await _productServive.Add(model);

            return RedirectToAction(nameof(Index));


        }

        //GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<CategoryViewModel> categories = await _categoryService.GetAll();
            SelectList selectLists = new SelectList(categories, "Id", "Name");
            ViewBag.SelectList = selectLists;
            return View(await _productServive.Edit(id));
        }

        // POST: Admin/Products/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            
            await _productServive.Update(model);
            return RedirectToAction("Index");    
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return View(await _productServive.Edit(id));
        }

        // POST: Admin/Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productServive.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
              
            }
           
        }

        
    }
}
