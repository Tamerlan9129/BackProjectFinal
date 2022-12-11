using Web.ViewModels.Basket;

namespace Web.Services.Abstract
{
    public interface IBasketService
    {
        Task<bool> Add(int productId);
        Task<bool> RemoveAsync(int id);
        Task<bool> ClearBasket(int id);
        Task<BasketIndexVM> GetBasketProducts();
        Task<bool> IncreaseCountAsync(int id);
        Task<bool> DecreaseCountAsync(int id);
    }
}
