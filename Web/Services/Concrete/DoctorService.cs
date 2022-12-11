using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.ViewModels.Doctor;

namespace Web.Services.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Doctor> DoctorDetailsAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            return doctor;
        }

        public async Task<DoctorIndexVM> GetAllDoctorAsync(DoctorIndexVM model, string? name)
        {
            var doctors = await _doctorRepository.FilterByName(name);
            var pageCount = await _doctorRepository.GetPageCountAsync(doctors, model.Take);
            doctors = await _doctorRepository.PaginateDoctorAsync(doctors, model.Page, model.Take);

            model = new DoctorIndexVM
            {
                Doctors = await doctors.ToListAsync(),
                Page = model.Page,
                Take = model.Take,
                PageCount = pageCount,
                Name = name,
            };
            return model;
        }
    }
}
