using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeVideo;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeVideoController : Controller
    {
        private readonly IHomeVideoService _homeVideoService;

        public HomeVideoController(IHomeVideoService homeVideoService)
        {
            _homeVideoService = homeVideoService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _homeVideoService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var isExist = await _homeVideoService.IsExistAsync();
            if (!isExist) return NotFound();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(HomeVideoCreateVM model)
        {
            var isSucceded = await _homeVideoService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index", "homevideo");
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var model = await _homeVideoService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, HomeVideoUpdateVM model)
        {
            var isSucceded = await _homeVideoService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "homevideo");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _homeVideoService.DeleteAsync(id);
            return RedirectToAction("index", "homevideo");
        }
    }
}
