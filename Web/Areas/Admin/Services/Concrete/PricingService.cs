using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Pricing;

namespace Web.Areas.Admin.Services.Concrete
{
    public class PricingService : IPricingService
    {
        private readonly IPricingRepository _pricingRepository;
        private readonly ModelStateDictionary _modelState;

        public PricingService(IPricingRepository pricingRepository,IActionContextAccessor actionContextAccessor)
        {
            _pricingRepository = pricingRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(PricingCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _pricingRepository.AnyAsync(p => p.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This title already exist");
                return false;
            }

            var pricing = new Pricing
            {
                Title = model.Title,
                Status = model.Status,
                SubTitle = model.SubTitle,
                Features = model.Features,
                CreatedAt = DateTime.Now,
                Price = model.Price,
                Period = model.Period
            };
            await _pricingRepository.CreateAsync(pricing);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var pricing = await _pricingRepository.GetAsync(id);
            await _pricingRepository.DeleteAsync(pricing);
        }

        public async Task<PricingIndexVM> GetAllAsync()
        {
            var pricing = await _pricingRepository.GetAllAsync();
            var model = new PricingIndexVM
            {
                Pricings = pricing
            };
            return model;
        }

        public async Task<PricingUpdateVM> GetUpdateModelAsync(int id)
        {
            var pricing = await _pricingRepository.GetAsync(id);
            var model = new PricingUpdateVM
            {
                Id = pricing.Id,
                Status = pricing.Status,
                SubTitle = pricing.SubTitle,
                Features = pricing.Features,
                Price = pricing.Price,
                Period = pricing.Period,
                Title = pricing.Title
            };
            return model;
        }

        public async Task<bool> UpdateAsync(PricingUpdateVM model)
        {
            var pricing = await _pricingRepository.GetAsync(model.Id);
            if (!_modelState.IsValid) return false;
            var isExist = await _pricingRepository.AnyAsync(p => p.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && model.Id != pricing.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This title already exist");
                return false;
            }

            pricing.Title = model.Title;
            pricing.SubTitle = model.SubTitle;
            pricing.Price = model.Price;
            pricing.Period = model.Period;
            pricing.Status = model.Status;
            pricing.Features = model.Features;
            pricing.ModifiedAt = DateTime.Now;

            await _pricingRepository.UpdateAsync(pricing);
            return true;
        }
    }
}
