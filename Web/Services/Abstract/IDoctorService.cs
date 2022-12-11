using Core.Entities;

using Web.ViewModels.Doctor;

namespace Web.Services.Abstract
{
    public interface IDoctorService
    {
        Task<DoctorIndexVM> GetAllDoctorAsync(DoctorIndexVM model,string? name);
    
        Task<Doctor> DoctorDetailsAsync(int id);
    }
}
