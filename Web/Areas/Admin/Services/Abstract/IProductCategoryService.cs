using Web.Areas.Admin.ViewModels.ProductCategory;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IProductCategoryService
    {
        Task<bool> CreateAsync(ProductCategoryCreateVM model);
        Task<bool> UpdateAsync(ProductCategoryUpdateVM model);
        Task<ProductCategoryUpdateVM> GetUpdateModelAsync(int id);
        Task<ProductCategoryIndexVM> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
