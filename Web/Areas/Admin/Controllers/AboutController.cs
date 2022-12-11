using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.About;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _aboutService.GetAsync();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var isExist = await _aboutService.IsExistAsync();
            if (isExist) return NotFound();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AboutCreateVM model)
        {
            var isSucceded = await _aboutService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index", "about");
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _aboutService.GetUpdateModelAsync(id);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, AboutUpdateVM model)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _aboutService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "About");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePhoto(int id)
        {
            var model = await _aboutService.GetUpdatePhotoModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoto(int id, AboutPhotoUpdateVM model)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _aboutService.UpdatePhotoAsync(model);
            if (isSucceded) return RedirectToAction("update", "about", new { id = model.AboutId });
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var aboutPhoto = await _aboutService.GetUpdatePhotoModelAsync(id);
            await _aboutService.DeletePhotoAsync(id);
            return RedirectToAction("update", "about", new { id = aboutPhoto.AboutId });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _aboutService.GetDetailsAsync(id);
            if (model == null) return NotFound();
            return View(model);

        }
    }
}
