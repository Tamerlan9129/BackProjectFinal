using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.QuestionCategory;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuestionCategoryController : Controller
    {
        private readonly IQuestionCategoryService _questionCategoryService;

        public QuestionCategoryController(IQuestionCategoryService questionCategoryService)
        {
            _questionCategoryService = questionCategoryService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _questionCategoryService.GetAllAsync();
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuestionCategoryCreateVM model)
        {
            var isSucceded = await _questionCategoryService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index", "questioncategory");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _questionCategoryService.GetUpdateModelAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, QuestionCategoryUpdateVM model)
        {
            var isSucceded = await _questionCategoryService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "questioncategory");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _questionCategoryService.DeleteAsync(id);
            return RedirectToAction("index", "questioncategory");
        }
    }
}
