using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Product;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
          _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _productService.GetProductsWithCategoryAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _productService.GetCategoriesCreateModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            var isSucceded = await _productService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index", "product");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _productService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductUpdateVM model)
        {
            if (id != model.Id) return BadRequest();
            var isSucceded = await _productService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction("index", "product");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction("index", "product");
        }
    }
}
