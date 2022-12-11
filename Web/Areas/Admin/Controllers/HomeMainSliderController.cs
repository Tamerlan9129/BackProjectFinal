using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeMainSliderController : Controller
    {
        private readonly IHomeMainSliderService _homeMainSliderService;

        public HomeMainSliderController(IHomeMainSliderService homeMainSliderService)
        {
            _homeMainSliderService = homeMainSliderService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _homeMainSliderService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(HomeMainSliderCreateVM model)
        {
            var isSucceded = await _homeMainSliderService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index", "homemainslider");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _homeMainSliderService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, HomeMainSliderUpdateVM model)
        {
            if (model.Id != id) return BadRequest();
            bool isSucceded = await _homeMainSliderService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "homemainslider");
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _homeMainSliderService.DeleteAsync(id);
            return RedirectToAction("index", "homemainslider");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, HomeMainSliderDetailsVM model)
        {
            if (model.Id != id) return BadRequest();
            model = await _homeMainSliderService.GetDetailsAsync(id);

            return View(model);
        }
    }
}
