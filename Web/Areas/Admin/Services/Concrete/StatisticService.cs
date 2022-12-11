
using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Statistic;

namespace Web.Areas.Admin.Services.Concrete
{
    public class StatisticService :  IStatisticService
    {
        private readonly IStatisticRepository _statisticRepository;
        private readonly ModelStateDictionary _modelState;

        public StatisticService(IStatisticRepository statisticRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _statisticRepository = statisticRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(StatisticCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _statisticRepository.AnyAsync(st => st.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This title already exist");
                return false;
            }
            var statistic = new Statistic
            {
                CreatedAt = DateTime.Now,
                Title = model.Title,
                Quantity = model.Quantity
            };
            await _statisticRepository.CreateAsync(statistic);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var statistic = await _statisticRepository.GetAsync(id);
            await _statisticRepository.DeleteAsync(statistic);
        }

        public async Task<StatisticIndexVM> GetAllAsync()
        {
            var statistic = await _statisticRepository.GetAllAsync();
            var model = new StatisticIndexVM
            {
                Statistics = statistic
            };
            return model;
        }

        public async Task<StatisticUpdateVM> GetUpdateModelAsync(int id)
        {
            var statistic = await _statisticRepository.GetAsync(id);
            var model = new StatisticUpdateVM
            {
                Id = statistic.Id,
                Quantity = statistic.Quantity,
                Title = statistic.Title
            };
            return model;
        }

        public async Task<bool> UpdateAsync(StatisticUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var statistic = await _statisticRepository.GetAsync(model.Id);
            var isExist = await _statisticRepository.AnyAsync(st => st.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && model.Id != st.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This title already exist");
                return false;
            }
            statistic.Title = model.Title;
            statistic.Quantity = model.Quantity;
            statistic.ModifiedAt = DateTime.Now;

            await _statisticRepository.UpdateAsync(statistic);
            return true;
        }
    }
}
