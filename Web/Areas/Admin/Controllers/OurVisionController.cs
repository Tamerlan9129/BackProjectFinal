using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.OurVision;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OurVisionController : Controller
    {
        private readonly IOurVisionService _ourVisionService;

        public OurVisionController(IOurVisionService ourVisionService)
        {
            _ourVisionService = ourVisionService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _ourVisionService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OurVisionCreateVM model)
        {
            var isSucceded = await _ourVisionService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index", "ourvision");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _ourVisionService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, OurVisionUpdateVM model)
        {
            var isSucceded = await _ourVisionService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "ourvision");
            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _ourVisionService.DeleteAsync(id);
            return RedirectToAction("index", "ourvision");
        }
    }
}
