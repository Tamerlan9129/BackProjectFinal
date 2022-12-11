using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.ViewModels.Question;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IQuestionService
    {
        Task<bool> CreateAsync(QuestionCreateVM model);
        Task<bool> UpdateAsync(QuestionUpdateVM model);
        Task<QuestionIndexVM> GetQuestionWithCategoryAsync();
        Task<List<SelectListItem>> GetCategoriesAsync();
        public Task<QuestionCreateVM> GetCategoriesCreateModelAsync();
        Task<QuestionUpdateVM> GetUpdateModelAsync(int id);
        Task DeleteAsync(int id);
    }
}
