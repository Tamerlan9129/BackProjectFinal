using Web.Areas.Admin.ViewModels.Doctor;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IDoctorService
    {
        Task<bool> CreateAsync(DoctorCreateVM model, List<string> SkillsList);
        Task<bool> UpdateAsync(DoctorUpdateVM model);
        Task<DoctorUpdateVM> GetUpdateModelAsync(int id);
        Task<DoctorDetailsVM> GetDetailsModelAsync(int id);
        Task DeleteAsync(int id);
        Task<DoctorIndexVM> GetAllAsync();
    }
}
