
using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels.Account;

namespace Web.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ModelStateDictionary _modelState;

        public AccountService(SignInManager<User> signInManager,UserManager<User> userManager, IActionContextAccessor actionContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> LoginAsync(AccountLoginVM model)
        {
            if (!_modelState.IsValid) return false;
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                _modelState.AddModelError(string.Empty, " Incorrect username or password");
                return false;
            }
            if(!await _userManager.IsInRoleAsync(user, UserRoles.User.ToString()))
            {
                _modelState.AddModelError(string.Empty, " Incorrect username or password");
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
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

        public async Task<bool> RegisterAsync(AccountRegisterVM model)
        {
            if(!_modelState.IsValid) return false;
            var user = new User
            {
                Fullname = model.Fullname,
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.Mobile
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception(error.Description.ToString());
                }
            }

            await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());
            return true;
        }
    }
}
