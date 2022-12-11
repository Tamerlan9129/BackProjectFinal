using Web.Areas.Admin.ViewModels.HomeVideo;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IHomeVideoService
    {
        Task<bool> CreateAsync(HomeVideoCreateVM model);
        Task<bool> UpdateAsync(HomeVideoUpdateVM model);
        Task<HomeVideoUpdateVM> GetUpdateModelAsync(int id);
        public Task<bool> IsExistAsync();
        Task<HomeVideoIndexVM> GetAsync();
        Task DeleteAsync(int id);
    }
}
