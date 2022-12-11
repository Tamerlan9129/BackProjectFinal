using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Services.Concrete
{
    public class HomeMainSliderService : IHomeMainSliderService
    {
        private readonly IHomeMainSliderRepository _homeMainSliderRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public HomeMainSliderService(IHomeMainSliderRepository homeMainSliderRepository,IActionContextAccessor actionContextAccessor,
            IFileService fileService)
        {
            _homeMainSliderRepository = homeMainSliderRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(HomeMainSliderCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _homeMainSliderRepository.AnyAsync(ms => ms.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This title already exist");
                return false;
            }
            int maxSize = 1500;

            if (!_fileService.CheckPhoto(model.Photo))
            {
                _modelState.AddModelError("Photos", $"{model.Photo.Name}  must be in image format");
                return false;
            }
            else if (!_fileService.MaxSize(model.Photo, maxSize))
            {
                _modelState.AddModelError("Photos", $"{model.Photo.Name} size over than {maxSize} kb");
                return false;
            }
            var count = await _homeMainSliderRepository.GetAllAsync();

            int order = count.Count();
            if (order == 0)
            {
                order = 1;
            }
            var mainSlider = new HomeMainSlider
            {
                Title = model.Title,
                Order = order++,
                Slogan = model.Slogan,
                ButtonLink = model.ButtonLink,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.Photo)
            };

            await _homeMainSliderRepository.CreateAsync(mainSlider);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var mainSlider = await _homeMainSliderRepository.GetAsync(id);
            if (mainSlider != null)
            {
                _fileService.Delete(mainSlider.PhotoName);
                await _homeMainSliderRepository.DeleteAsync(mainSlider);
            }
        }

        public async Task<HomeMainSliderIndexVM> GetAllAsync()
        {
            var model = new HomeMainSliderIndexVM
            {
                HomeMainSliders = await _homeMainSliderRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<HomeMainSliderDetailsVM> GetDetailsAsync(int id)
        {
            var slider = await _homeMainSliderRepository.GetAsync(id);
            var model = new HomeMainSliderDetailsVM
            {
                Id = slider.Id,
                Title = slider.Title,
                Slogan = slider.Slogan,
                ButtonLink = slider.ButtonLink,
                Order = slider.Order,
                PhotoName = slider.PhotoName,
                CreatedAt = slider.CreatedAt,
                ModifiedAt = slider.ModifiedAt
            };
            return model;
        }

        public async Task<HomeMainSliderUpdateVM> GetUpdateModelAsync(int id)
        {
            var slider = await _homeMainSliderRepository.GetAsync(id);
            var model = new HomeMainSliderUpdateVM
            {
                Id = slider.Id,
                Title = slider.Title,
                Slogan = slider.Slogan,
                ButtonLink = slider.ButtonLink,
                Order = slider.Order
            };
            return model;
        }

        public async Task<bool> UpdateAsync(HomeMainSliderUpdateVM model)
        {
            var sliderUpdate = await _homeMainSliderRepository.GetAsync(model.Id);
            var isExist = await _homeMainSliderRepository.AnyAsync(ms => ms.Title.Trim().ToLower() == model.Title.Trim().ToLower() && ms.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This slider already exist");
                return false;
            }
            sliderUpdate.Title = model.Title;
            sliderUpdate.Slogan = model.Slogan;
            sliderUpdate.ButtonLink = model.ButtonLink;
            sliderUpdate.Order = model.Order;
            sliderUpdate.ModifiedAt = DateTime.Now;



            if (model.Photo != null)
            {
                int maxSize = 1000;
                if (!_fileService.CheckPhoto(model.Photo))
                {
                    _modelState.AddModelError("Photo", "The photo must be in image format");
                    return false;
                }
                else if (!_fileService.MaxSize(model.Photo, maxSize))
                {
                    _modelState.AddModelError("Photo", $"The photo size over than {maxSize}kb");
                    return true;
                }
                sliderUpdate.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            _fileService.Delete(sliderUpdate.PhotoName);
            await _homeMainSliderRepository.UpdateAsync(sliderUpdate);
            return true;
        }
    }
}
