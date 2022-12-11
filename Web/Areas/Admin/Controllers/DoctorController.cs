using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Doctor;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _doctorService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateVM model, List<string> SkillsList)
        {

            var isSucceded = await _doctorService.CreateAsync(model, SkillsList);
            if (isSucceded) return RedirectToAction("index", "doctor");
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _doctorService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DoctorUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _doctorService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "doctor");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _doctorService.DeleteAsync(id);
            return RedirectToAction("index", "doctor");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _doctorService.GetDetailsModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
    }
}
