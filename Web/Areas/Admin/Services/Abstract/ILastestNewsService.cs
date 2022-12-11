

using Web.Areas.Admin.ViewModels.LatestNews;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ILastestNewsService
    {
        Task<bool> CreateAsync(LastestNewsCreateVM model);
        Task<bool> UpdateAsync(LastestNewsUpdateVM model);
        Task<LastestNewsUpdateVM> GetUpdateModelAsync(int id);
        Task<LastestNewsIndexVM> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
