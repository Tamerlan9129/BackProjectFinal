using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Components;

namespace Web.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IBasketProductRepository _basketProductRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HeaderViewComponent(IBasketProductRepository basketProductRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _basketProductRepository = basketProductRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new HeaderComponentVM
            {
                Count =await _basketProductRepository.GetUserBasketProductsCount(_httpContextAccessor.HttpContext.User)
            };
            return View(model);
        }
    }

}
