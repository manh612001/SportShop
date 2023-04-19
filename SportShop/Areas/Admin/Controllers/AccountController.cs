using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportShop.Interface;
using SportShop.ViewModels.Account;

namespace SportShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class AccountController : Controller
    {

        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            await _accountService.Login(model);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _accountService.Logout();
            return RedirectToAction("Login");
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
