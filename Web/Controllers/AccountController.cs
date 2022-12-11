using Core.Utilities.Attributes;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Account;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [OnlyAnonimous]

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [OnlyAnonimous]
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            var isSucceded = await _accountService.LoginAsync(model);
            if (!string.IsNullOrEmpty(model.ReturnUrl)) return Redirect(model.ReturnUrl);
            if (isSucceded) return RedirectToAction("index", "home");
            return View(model);
        }
        [OnlyAnonimous]
        [HttpGet]
        public async Task<IActionResult> Register()
        {

            return View();
        }
        [OnlyAnonimous]
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterVM model)
        {
            var isSucceded = await _accountService.RegisterAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Login));
            return View(model);
        }
        public async Task<IActionResult> LogOut()
        {
            await _accountService.LogOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
