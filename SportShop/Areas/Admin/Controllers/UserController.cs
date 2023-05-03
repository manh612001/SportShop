using Microsoft.AspNetCore.Mvc;
using SportShop.Interface;
using SportShop.ViewModels.Account;

namespace SportShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;
        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _accountService.GetAll());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            await _accountService.SignUp(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(string? id)
        {
            return View(await _accountService.GetById(id));
        }
        public async Task<IActionResult> Edit(string? id)
        {
            return View(await _accountService.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AddUserViewModel model)
        {
            await _accountService.Update(model);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string? id)
        {
            return View(await _accountService.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCF(string? id)
        {
            await _accountService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
