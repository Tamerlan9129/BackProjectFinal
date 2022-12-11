using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeVideo;

namespace Web.Areas.Admin.Services.Concrete
{
    public class HomeVideoService : IHomeVideoService
    {
        private readonly IFileService _fileService;
        private readonly IHomeVideoRepository _homeVideoRepository;
        private readonly ModelStateDictionary _modelState;

        public HomeVideoService(IHomeVideoRepository homeVideoRepository,IFileService fileService,IActionContextAccessor actionContextAccessor)
        {
            _fileService = fileService;
            _homeVideoRepository = homeVideoRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(HomeVideoCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            int maxSize = 1500;
            if (!_fileService.CheckPhoto(model.CoverPhoto))
            {
                _modelState.AddModelError("CoverPhoto", "The photo must be in image format");
                return false;
            }
            else if (!_fileService.MaxSize(model.CoverPhoto, maxSize))
            {
                _modelState.AddModelError("CoverPhoto", $"The photo size over than {maxSize} kb");
                return false;
            }
            var homeVideo = new HomeVideo
            {
                CreatedAt = DateTime.Now,
                Link = model.Link,
                PhotoName = await _fileService.UploadAsync(model.CoverPhoto)
            };
            await _homeVideoRepository.CreateAsync(homeVideo);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteModel = await _homeVideoRepository.GetAsync(id);
            await _homeVideoRepository.DeleteAsync(deleteModel);
        }

        public async Task<HomeVideoIndexVM> GetAsync()
        {
            var homeVideo = await _homeVideoRepository.GetAsync();
            var model = new HomeVideoIndexVM
            {
                HomeVideo = homeVideo,
            };
            return model;
        }

        public async Task<HomeVideoUpdateVM> GetUpdateModelAsync(int id)
        {
            var homeVideo = await _homeVideoRepository.GetAsync(id);
            var model = new HomeVideoUpdateVM
            {
                Id = homeVideo.Id,
                Link = homeVideo.Link
            };
            return model;
        }

        public async Task<bool> IsExistAsync()
        {
            var isExist = await _homeVideoRepository.GetAsync();
            if (isExist != null) return false;
            return true;
        }

        public async Task<bool> UpdateAsync(HomeVideoUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var updateHomeVideo = await _homeVideoRepository.GetAsync(model.Id);
            updateHomeVideo.Link = model.Link;
            updateHomeVideo.ModifiedAt = DateTime.Now;
            if (model.CoverPhoto != null)
            {
                int maxSize = 1500;
                if (!_fileService.CheckPhoto(model.CoverPhoto))
                {
                    _modelState.AddModelError("CoverPhoto", "The photo must be in image format");
                    return false;
                }
                else if (!_fileService.MaxSize(model.CoverPhoto, maxSize))
                {
                    _modelState.AddModelError("CoverPhoto", $"The photo size over than {maxSize} kb");
                    return false;
                }
                _fileService.Delete(updateHomeVideo.PhotoName);
                updateHomeVideo.PhotoName = await _fileService.UploadAsync(model.CoverPhoto);
            }
            await _homeVideoRepository.UpdateAsync(updateHomeVideo);
            return true;
        }
    }
}
