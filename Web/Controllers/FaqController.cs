using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Faq;

namespace Web.Controllers
{
    public class FaqController : Controller
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }
        public async Task<IActionResult> Index(FaqIndexVM model)
        {
            var questions = await _faqService.GetQuestionsAsync(model);
            model.Questions = questions;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> FilterByCategory(int id)
        {
            var questions = await _faqService.FilterByCategoryId(id);
            var model = new FaqIndexVM
            {
                Questions = questions
            };
            return PartialView("_QuestionPartial", model);
        }
    }
}
