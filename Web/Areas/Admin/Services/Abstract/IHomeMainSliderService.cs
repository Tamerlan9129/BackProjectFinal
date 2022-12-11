using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IHomeMainSliderService
    {
        Task<bool> CreateAsync(HomeMainSliderCreateVM model);
        Task<bool> UpdateAsync(HomeMainSliderUpdateVM model);
        Task<HomeMainSliderUpdateVM> GetUpdateModelAsync(int id);
        Task<HomeMainSliderDetailsVM> GetDetailsAsync(int id);
        Task DeleteAsync(int id);
        Task<HomeMainSliderIndexVM> GetAllAsync();
    }
}
