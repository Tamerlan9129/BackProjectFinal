using Web.Areas.Admin.ViewModels.About;
using Web.Areas.Admin.ViewModels.HomeMainSlider;
using Web.Areas.Admin.ViewModels.MedicalDepartment;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IMedicalDepartmentService
    {
        Task<bool> CreateAsync(MedicalDepartmentCreateVM model);
        Task<MedicalDepartmentIndexVM> GetAllAsync();
        Task<MedicalDepartmentUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(MedicalDepartmentUpdateVM model);
        Task DeleteAsync(int id);

    }
}
