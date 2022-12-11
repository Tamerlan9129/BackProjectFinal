using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.ProductCategory;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
           _productCategoryService = productCategoryService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _productCategoryService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCategoryCreateVM model)
        {
            var isSucceded = await _productCategoryService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index", "productcategory");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _productCategoryService.GetUpdateModelAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductCategoryUpdateVM model)
        {
            if (id != model.Id) return BadRequest();
            var isSucceded = await _productCategoryService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "productcategory");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _productCategoryService.DeleteAsync(id);
            return RedirectToAction("index", "productcategory");
        }
    }
}
