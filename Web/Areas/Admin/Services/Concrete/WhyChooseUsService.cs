using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.WhyChooseUs;

namespace Web.Areas.Admin.Services.Concrete
{
    public class WhyChooseUsService : IWhyChooseUsService
    {
        private readonly IWhyChooseUsRepository _whyChooseUsRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public WhyChooseUsService(IWhyChooseUsRepository whyChooseUsRepository,IFileService fileService,IActionContextAccessor actionContextAccessor)
        {
            _whyChooseUsRepository = whyChooseUsRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(WhyChooseUsCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _whyChooseUsRepository.GetAsync();
            if (isExist != null) return false;
            int maxSize = 1500;
            if (!_fileService.CheckPhoto(model.Photo))
            {
                _modelState.AddModelError("Photo", "Photo must be in image format");
                return false;
            }
            else if (!_fileService.MaxSize(model.Photo, maxSize))
            {
                _modelState.AddModelError("Photo", $"Photo size over than {maxSize} kb");
                return false;
            }

            var whyChooseUs = new WhyChooseUs
            {

                Title = model.Title,
                Description = model.Description,
                Services = model.Services,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.Photo)

            };
            await _whyChooseUsRepository.CreateAsync(whyChooseUs);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var whyChooseUs = await _whyChooseUsRepository.GetAsync(id);
            await _whyChooseUsRepository.DeleteAsync(whyChooseUs);
        }

        public async Task<WhyChooseUsIndexVM> GetAsync()
        {
            var model = new WhyChooseUsIndexVM
            {
                WhyChooseUs = await _whyChooseUsRepository.GetAsync()
            };
            return model;
        }

        public async Task<WhyChooseUsDetailsVM> GetDetailsModelAsync(int id)
        {
            var whyChooseUs = await _whyChooseUsRepository.GetAsync(id);
            var model = new WhyChooseUsDetailsVM
            {
                Id = whyChooseUs.Id,
                Title = whyChooseUs.Title,
                Description = whyChooseUs.Description,
                Services = whyChooseUs.Services,
                PhotoName = whyChooseUs.PhotoName,
                CreatedAt = whyChooseUs.CreatedAt,
                ModifiedAt = whyChooseUs.ModifiedAt
            };
            return model;
        }

        public async Task<WhyChooseUsUpdateVM> GetUpdateModelAsync(int id)
        {
            var whyChooseUs = await _whyChooseUsRepository.GetAsync(id);
            var model = new WhyChooseUsUpdateVM
            {

                Id = whyChooseUs.Id,
                Title = whyChooseUs.Title,
                Description = whyChooseUs.Description,
                Services = whyChooseUs.Services
            };
            return model;
        }

        public async Task<bool> IsExistAsync()
        {
            var isExist = await _whyChooseUsRepository.GetAsync();
            if (isExist != null) return false;
            return true;
        }

        public async Task<bool> UpdateAsync(WhyChooseUsUpdateVM model)
        {
            var whychooseUs = await _whyChooseUsRepository.GetAsync(model.Id);
            if (whychooseUs == null) return false;
            whychooseUs.Description = model.Description;
            whychooseUs.Services = model.Services;
            whychooseUs.Title = model.Title;
            whychooseUs.ModifiedAt = DateTime.Now;


            if (model.Photo != null)
            {
                int maxSize = 1500;
                if (!_fileService.CheckPhoto(model.Photo))
                {
                    _modelState.AddModelError("Photo", "Photo must be in image format");
                    return false;
                }
                else if (!_fileService.MaxSize(model.Photo, maxSize))
                {
                    _modelState.AddModelError("Photo", $"Photo size over than {maxSize} kb");
                    return false;
                }
                _fileService.Delete(whychooseUs.PhotoName);
                whychooseUs.PhotoName = await _fileService.UploadAsync(model.Photo);
            }

            await _whyChooseUsRepository.UpdateAsync(whychooseUs);
            return true;
        }
    }
}
