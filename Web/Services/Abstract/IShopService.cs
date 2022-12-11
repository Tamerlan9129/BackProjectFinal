using Web.ViewModels.Shop;

namespace Web.Services.Abstract
{
    public interface IShopService
    {
        Task<ShopIndexVM> GetAllProductsWithCategoriesAsync(ShopIndexVM model);
    }
}
