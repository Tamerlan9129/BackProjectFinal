using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Question;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _questionService.GetQuestionWithCategoryAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _questionService.GetCategoriesCreateModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuestionCreateVM model)
        {
            var isSucceded = await _questionService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index", "question");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _questionService.GetUpdateModelAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, QuestionUpdateVM model)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _questionService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "question");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _questionService.DeleteAsync(id);
            return RedirectToAction("index", "question");
        }
    }
}
