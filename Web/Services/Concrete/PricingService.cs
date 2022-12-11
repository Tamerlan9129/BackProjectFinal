using DataAccess.Repositories.Abstract;
using Web.Services.Abstract;
using Web.ViewModels.Pricing;

namespace Web.Services.Concrete
{
    public class PricingService : IPricingService
    {
        private readonly IPricingRepository _pricingRepository;

        public PricingService(IPricingRepository pricingRepository)
        {
            _pricingRepository = pricingRepository;
        }

        public async Task<PricingIndexVM> GetAllAsync()
        {
            var pricings = await _pricingRepository.GetAllAsync();
            var model = new PricingIndexVM
            {
                Pricings = pricings
            };
            return model;
        }
    }
}
