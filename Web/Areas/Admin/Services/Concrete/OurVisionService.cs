using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.OurVision;

namespace Web.Areas.Admin.Services.Concrete
{
    public class OurVisionService : IOurVisionService
    {
        private readonly IOurVisionRepository _ourVisionRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public OurVisionService(IOurVisionRepository ourVisionRepository,IFileService fileService,IActionContextAccessor actionContextAccessor)
        {
           _ourVisionRepository = ourVisionRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(OurVisionCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _ourVisionRepository.AnyAsync(ov => ov.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This title already exist");
                return false;
            }
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
            var ourVision = new OurVision
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                PhotoName = await _fileService.UploadAsync(model.Photo)
            };

            await _ourVisionRepository.CreateAsync(ourVision);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var ourVision = await _ourVisionRepository.GetAsync(id);
            if (ourVision != null)
            {
                await _ourVisionRepository.DeleteAsync(ourVision);
            }
        }

        public async Task<OurVisionIndexVM> GetAllAsync()
        {
            var model = new OurVisionIndexVM
            {
                OurVision = await _ourVisionRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<OurVisionUpdateVM> GetUpdateModelAsync(int id)
        {
            var ourVision = await _ourVisionRepository.GetAsync(id);
            var model = new OurVisionUpdateVM
            {
                Title = ourVision.Title,
                Description = ourVision.Description
            };
            return model;
        }

        public async Task<bool> UpdateAsync(OurVisionUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var ourVision = await _ourVisionRepository.GetAsync(model.Id);
            var isExist = await _ourVisionRepository.AnyAsync(ov => ov.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && model.Id != ov.Id);

            if (isExist)
            {
                _modelState.AddModelError("Title", "This title already exist");
                return false;
            }
            if (ourVision != null)
            {
                ourVision.Description = model.Description;
                ourVision.Title = model.Title;
                ourVision.ModifiedAt = DateTime.Now;
            }
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
                _fileService.Delete(ourVision.PhotoName);
                ourVision.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _ourVisionRepository.UpdateAsync(ourVision);
            return true;
        }
    }
}
