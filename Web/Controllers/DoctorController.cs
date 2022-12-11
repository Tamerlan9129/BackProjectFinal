using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Web.Services.Abstract;
using Web.ViewModels.Doctor;

namespace Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public async Task<IActionResult> Index(DoctorIndexVM model,string? name)
        {
            model = await _doctorService.GetAllDoctorAsync(model,name);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _doctorService.DoctorDetailsAsync(id);
            return View(doctor);
        }

       
    }
}
