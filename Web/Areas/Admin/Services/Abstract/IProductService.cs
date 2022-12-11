using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.ViewModels.Product;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IProductService
    {
        Task<bool> CreateAsync(ProductCreateVM model);
        Task<bool> UpdateAsync(ProductUpdateVM model);
        Task<ProductUpdateVM> GetUpdateModelAsync(int id);
        Task<ProductIndexVM> GetAllAsync();
        Task<ProductIndexVM> GetProductsWithCategoryAsync();
        Task<List<SelectListItem>> GetCategoriesAsync();
        public Task<ProductCreateVM> GetCategoriesCreateModelAsync();
        Task DeleteAsync(int id);
    }
}
