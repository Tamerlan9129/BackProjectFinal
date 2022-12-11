using Web.ViewModels.Pricing;

namespace Web.Services.Abstract
{
    public interface IPricingService
    {
        Task<PricingIndexVM> GetAllAsync();
    }
}
