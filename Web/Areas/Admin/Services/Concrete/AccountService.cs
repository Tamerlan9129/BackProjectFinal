using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Account;

namespace Web.Areas.Admin.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ModelStateDictionary _modelState;

        public AccountService(SignInManager<User> signInManager,UserManager<User> userManager,IActionContextAccessor actionContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> LoginAsync(AccountLoginVM model)
        {
            if (!_modelState.IsValid) return false;
            var admin = await _userManager.FindByNameAsync(model.Username);
            if (admin == null)
            {
                _modelState.AddModelError(string.Empty, " Incorrect username or password");
                return false;
            }
            if (!await _userManager.IsInRoleAsync(admin, UserRoles.Admin.ToString()))
            {
                _modelState.AddModelError(string.Empty, " Incorrect username or password");
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (!result.Succeeded)
            {
                _modelState.AddModelError(string.Empty, " Incorrect username or password");
                return false;
            }
            return true;
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
