using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.About;

namespace Web.Areas.Admin.Services.Concrete
{
    public class AboutService : IAboutService
    {
        private readonly IAboutRepository _aboutRepository;
        private readonly IAboutPhotoRepository _aboutPhotoRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public AboutService(IAboutRepository aboutRepository,
            IAboutPhotoRepository aboutPhotoRepository,IFileService fileService,IActionContextAccessor actionContextAccessor)
        {
            _aboutRepository = aboutRepository;
           _aboutPhotoRepository = aboutPhotoRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(AboutCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _aboutRepository.GetAsync();
            if (isExist != null)
            {
                _modelState.AddModelError("Title", "This title already exist ");
                return false;
            }
            int maxSize = 1500;
            if (!_fileService.CheckPhoto(model.IconPhoto))
            {
                _modelState.AddModelError("IconPhoto", "The photo must be in image format");
                return false;
            }
            else if (!_fileService.MaxSize(model.IconPhoto, maxSize))
            {
                _modelState.AddModelError("IconPhoto", $"The photo size over than {maxSize} kb");
                return false;
            }

            var about = new About
            {
                Title = model.Title,
                Header = model.Header,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                IconName = await _fileService.UploadAsync(model.IconPhoto)
            };

            await _aboutRepository.CreateAsync(about);

            bool hasError = false;
            int order = 1;
            foreach (var photo in model.Photos)
            {
                if (!_fileService.CheckPhoto(photo))
                {
                    _modelState.AddModelError("Photo", $"{photo.Name} must be in image format");
                    hasError = true;
                }
                else if (!_fileService.MaxSize(photo, maxSize))
                {
                    _modelState.AddModelError("Photo", $"{photo.Name} size over than {maxSize}kb");
                    hasError = true;
                }
            }
            if (hasError) return false;

            foreach (var checkePhoto in model.Photos)
            {
                var aboutPhoto = new AboutPhoto
                {
                    Order = order++,
                    AboutId = about.Id,
                    CreatedAt = DateTime.Now,
                    PhotoName = await _fileService.UploadAsync(checkePhoto)
                };
                await _aboutPhotoRepository.CreateAsync(aboutPhoto);
            }
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var about = await _aboutRepository.GetAsync(id);
            if (about != null)
                await _aboutRepository.DeleteAsync(about);
        }

        public async Task DeletePhotoAsync(int id)
        {
            var aboutPhoto = await _aboutPhotoRepository.GetAsync(id);
            await _aboutPhotoRepository.DeleteAsync(aboutPhoto);
        }

        public async Task<AboutIndexVM> GetAsync()
        {
            var about = await _aboutRepository.GetAsync();
            var model = new AboutIndexVM
            {
                About = about,
                Photos = await _aboutPhotoRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<AboutDetailsVM> GetDetailsAsync(int id)
        {
            var aboutUs = await _aboutRepository.GetAsync(id);

            var model = new AboutDetailsVM
            {
                Id = aboutUs.Id,
                Header = aboutUs.Header,
                Title = aboutUs.Title,
                CreatedAt = aboutUs.CreatedAt,
                ModifiedAt = aboutUs.ModifiedAt,
                Description = aboutUs.Description,
                IconPhoto = aboutUs.IconName,
                Photos = await _aboutPhotoRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<AboutUpdateVM> GetUpdateModelAsync(int id)
        {
            var about = await _aboutRepository.GetAsync(id);
            var model = new AboutUpdateVM
            {
                Id = about.Id,
                Title = about.Title,
                Header = about.Header,
                Description = about.Description,
                AboutPhoto = await _aboutPhotoRepository.GetAllAsync(),
            };
            return model;
        }

        public async Task<AboutPhotoUpdateVM> GetUpdatePhotoModelAsync(int id)
        {
            var aboutPhoto = await _aboutPhotoRepository.GetAsync(id);
            var model = new AboutPhotoUpdateVM
            {
                Id = aboutPhoto.Id,
                Order = aboutPhoto.Order,
                AboutId = aboutPhoto.AboutId,
            };
            return model;
        }

        public async Task<bool> IsExistAsync()
        {
            var isExist = await _aboutRepository.GetAsync();
            if (isExist == null) return false;
            return true;
        }

        public async Task<bool> UpdateAsync(AboutUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var about = await _aboutRepository.GetAsync(model.Id);
            if (about == null) return false;

            about.Description = model.Description;
            about.Title = model.Title;
            about.ModifiedAt = DateTime.Now;


            int maxSize = 1500;

            if (model.IconPhoto != null)
            {

                if (!_fileService.CheckPhoto(model.IconPhoto))
                {
                    _modelState.AddModelError("IconPhoto", "The photo must be in image format");
                    return false;
                }
                else if (!_fileService.MaxSize(model.IconPhoto, maxSize))
                {
                    _modelState.AddModelError("IconPhoto", $"The photo size over than {maxSize}kb");
                    return false;
                }
                _fileService.Delete(about.IconName);
                about.IconName = await _fileService.UploadAsync(model.IconPhoto);
            }
            if (model.Photos != null)
            {
                bool hasError = false;
                foreach (var photo in model.Photos)
                {
                    if (!_fileService.CheckPhoto(model.IconPhoto))
                    {
                        _modelState.AddModelError("Photos", $"{photo.FileName} must be in image format");
                        hasError = true;
                    }
                    else if (!_fileService.MaxSize(model.IconPhoto, maxSize))
                    {
                        _modelState.AddModelError("Photos", $"{photo.FileName} size over than {maxSize}kb");
                        hasError = true;
                    }

                }

                if (hasError) return false;

                foreach (var photo in model.Photos)
                {
                    var aboutPhoto = await _aboutPhotoRepository.GetAsync();
                    _fileService.Delete(aboutPhoto.PhotoName);
                    aboutPhoto.PhotoName = await _fileService.UploadAsync(photo);
                    await _aboutPhotoRepository.UpdateAsync(aboutPhoto);
                }


            }
            await _aboutRepository.UpdateAsync(about);
            return true;
        }

        public async Task<bool> UpdatePhotoAsync(AboutPhotoUpdateVM model)
        {
            var aboutPhoto = await _aboutPhotoRepository.GetAsync(model.Id);
            model.AboutId = aboutPhoto.AboutId;
            aboutPhoto.Order = model.Order;
            aboutPhoto.ModifiedAt = DateTime.Now;
            await _aboutPhotoRepository.UpdateAsync(aboutPhoto);
            return true;
        }
    }
}
