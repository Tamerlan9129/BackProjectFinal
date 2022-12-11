using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Basket;

namespace Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _basketService.GetBasketProducts();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BasketAddVM model)
        {
            if (await _basketService.Add(model.Id)) return Ok();
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _basketService.RemoveAsync(id)) return Ok();
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ClearBasket(int id)
        {
            if (await _basketService.ClearBasket(id)) return RedirectToAction("index", "basket");
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> IncreaseCount(int id)
        {
            if (await _basketService.IncreaseCountAsync(id)) return Ok();
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseCount(int id)
        {
            if (await _basketService.DecreaseCountAsync(id)) return Ok();
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> MiniBasket()
        {
            var model = await _basketService.GetBasketProducts();
            return PartialView("_MiniBasketPartial", model);
        }

    }
}
