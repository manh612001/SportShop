using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportShop.Interface;
using SportShop.ViewModels.Account;

namespace SportShop.Areas.Admin.Controllers
{
    
    public class AccountController : Controller
    {

        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        
        public IActionResult Login(string ReturnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = ReturnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var url = await _accountService.Login(model);
            return Redirect(url);
        }
        public async Task<IActionResult> LogOut()
        {
            await _accountService.Logout();
            return Redirect("/Account/Login");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(AddUserViewModel model)
        {
            try
            {
               
                await _accountService.SignUp(model);
                return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}
