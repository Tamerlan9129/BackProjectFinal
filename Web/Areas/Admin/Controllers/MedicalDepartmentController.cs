using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.MedicalDepartment;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MedicalDepartmentController : Controller
    {
        private readonly IMedicalDepartmentService _medicalDepartmentService;

        public MedicalDepartmentController(IMedicalDepartmentService medicalDepartmentService)
        {
            _medicalDepartmentService = medicalDepartmentService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _medicalDepartmentService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicalDepartmentCreateVM model)
        {
            var isSucceded = await _medicalDepartmentService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index", "medicaldepartment");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _medicalDepartmentService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, MedicalDepartmentUpdateVM model)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _medicalDepartmentService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "medicaldepartment");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _medicalDepartmentService.DeleteAsync(id);
            return RedirectToAction("index", "medicaldepartment");

        }
    }
}
