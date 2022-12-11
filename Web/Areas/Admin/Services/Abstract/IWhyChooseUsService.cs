using Web.Areas.Admin.ViewModels.WhyChooseUs;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IWhyChooseUsService
    {
        Task<bool> CreateAsync(WhyChooseUsCreateVM model);
        Task<bool> UpdateAsync(WhyChooseUsUpdateVM model);
        Task<WhyChooseUsUpdateVM> GetUpdateModelAsync(int id);
        Task<WhyChooseUsDetailsVM> GetDetailsModelAsync(int id);
        Task<bool> IsExistAsync();
        Task DeleteAsync(int id);
        Task<WhyChooseUsIndexVM> GetAsync();
    }
}
