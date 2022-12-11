using Web.Areas.Admin.ViewModels.QuestionCategory;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IQuestionCategoryService
    {
        Task<bool> CreateAsync(QuestionCategoryCreateVM model);
        Task<bool> UpdateAsync(QuestionCategoryUpdateVM model);
        Task<QuestionCategoryUpdateVM> GetUpdateModelAsync(int id);
        Task<QuestionCategoryIndexVM> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
