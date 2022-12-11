using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.LatestNews;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LastestNewsController : Controller
    {
        private readonly ILastestNewsService _lastestNewsService;

        public LastestNewsController(ILastestNewsService lastestNewsService)
        {
            _lastestNewsService = lastestNewsService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _lastestNewsService.GetAllAsync();
            return View(model);
        }
        [HttpGet]

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(LastestNewsCreateVM model)
        {
            var isSucceded = await _lastestNewsService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index", "lastestnews");
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var model = await _lastestNewsService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, LastestNewsUpdateVM model)
        {
            var isSucceded = await _lastestNewsService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "lastestnews");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _lastestNewsService.DeleteAsync(id);
            return RedirectToAction("index", "lastestnews");
        }
    }
}
