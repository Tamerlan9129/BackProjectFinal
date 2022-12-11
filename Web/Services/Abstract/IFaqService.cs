using Core.Entities;
using Web.ViewModels.Faq;

namespace Web.Services.Abstract
{
    public interface IFaqService
    {
        Task<List<Question>> GetQuestionsAsync(FaqIndexVM model);

        Task<List<Question>> FilterByCategoryId(int id);
    }
}
