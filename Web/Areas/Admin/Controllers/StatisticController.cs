using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Statistic;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StatisticController : Controller
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _statisticService.GetAllAsync();
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(StatisticCreateVM model)
        {
            var isSucceded = await _statisticService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index", "statistic");
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var model = await _statisticService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, StatisticUpdateVM model)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _statisticService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "statistic");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _statisticService.DeleteAsync(id);
            return RedirectToAction("index", "statistic");
        }
    }
}
