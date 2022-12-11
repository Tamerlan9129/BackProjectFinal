using Core.Entities;
using DataAccess.Repositories.Abstract;
using Web.Services.Abstract;
using Web.ViewModels.Faq;

namespace Web.Services.Concrete
{
    public class FaqService : IFaqService
    {
        private readonly IQuestionRepository _questionRepository;

        public FaqService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public Task<List<Question>> FilterByCategoryId(int id)
        {
            var questions = _questionRepository.FilterByCategory(id);
            return questions;
        }

        public async Task<List<Question>> GetQuestionsAsync(FaqIndexVM model)
        {
            var questions = await _questionRepository.GetQuestionsWithCategory();

            return questions;
        }
    }
}
