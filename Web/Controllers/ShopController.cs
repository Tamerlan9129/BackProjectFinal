using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Shop;

namespace Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }
        public async Task<IActionResult> Index(ShopIndexVM model)
        {
            model = await _shopService.GetAllProductsWithCategoriesAsync(model);
            return View(model);
        }
    }
}
