using Web.Areas.Admin.ViewModels.Statistic;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IStatisticService
    {
        Task<bool> CreateAsync(StatisticCreateVM model);

        Task<bool> UpdateAsync(StatisticUpdateVM model);

        Task<StatisticUpdateVM> GetUpdateModelAsync(int id);

        Task<StatisticIndexVM> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
