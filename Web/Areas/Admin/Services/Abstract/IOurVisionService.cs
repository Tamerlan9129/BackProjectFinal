using Web.Areas.Admin.ViewModels.OurVision;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IOurVisionService
    {
        Task<bool> CreateAsync(OurVisionCreateVM model);
        Task<bool> UpdateAsync(OurVisionUpdateVM model);
        Task<OurVisionUpdateVM> GetUpdateModelAsync(int id);
        Task DeleteAsync(int id);
        Task<OurVisionIndexVM> GetAllAsync();
    }
}
