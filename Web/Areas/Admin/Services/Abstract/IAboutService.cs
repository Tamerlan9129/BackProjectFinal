using Web.Areas.Admin.ViewModels.About;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IAboutService
    {
        Task<AboutIndexVM> GetAsync();
        Task<bool> CreateAsync(AboutCreateVM model);
        Task<AboutUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(AboutUpdateVM model);
        Task<bool> IsExistAsync();
        Task<AboutDetailsVM> GetDetailsAsync(int id);
        Task DeleteAsync(int id);
        Task<AboutPhotoUpdateVM> GetUpdatePhotoModelAsync(int id);
        Task<bool> UpdatePhotoAsync(AboutPhotoUpdateVM model);
        Task DeletePhotoAsync(int id);
    }
}
