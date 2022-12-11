using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.WhyChooseUs;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WhyChooseUsController : Controller
    {
        private readonly IWhyChooseUsService _whyChooseUsService;

        public WhyChooseUsController(IWhyChooseUsService whyChooseUsService)
        {
            _whyChooseUsService = whyChooseUsService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _whyChooseUsService.GetAsync();
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            var isExist = await _whyChooseUsService.IsExistAsync();
            if (!isExist) return NotFound();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WhyChooseUsCreateVM model)
        {
            var isSucceded = await _whyChooseUsService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index", "whychooseus");
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var model = await _whyChooseUsService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, WhyChooseUsUpdateVM model)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _whyChooseUsService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "whychooseus");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, WhyChooseUsDetailsVM model)
        {
            if (model.Id != id) return BadRequest();
            model = await _whyChooseUsService.GetDetailsModelAsync(id);
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _whyChooseUsService.DeleteAsync(id);
            return RedirectToAction("index", "whychooseus");
        }
    }
}
