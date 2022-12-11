using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class PricingController : Controller
    {
        private readonly IPricingService _pricingService;

        public PricingController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _pricingService.GetAllAsync();
            return View(model);
        }
    }
}
