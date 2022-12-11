using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.MedicalDepartment;

namespace Web.Areas.Admin.Services.Concrete
{
    public class MedicalDepartmentService : IMedicalDepartmentService
    {
        private readonly IFileService _fileService;
        private readonly IMedicalDepartmentRepository _medicalDepartmentRepository;
        private readonly ModelStateDictionary _modelState;

        public MedicalDepartmentService(IFileService fileService,IMedicalDepartmentRepository medicalDepartmentRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _fileService = fileService;
            _medicalDepartmentRepository = medicalDepartmentRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(MedicalDepartmentCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _medicalDepartmentRepository.AnyAsync(md => md.Title.Trim().ToLower() == model.Title.Trim().ToLower());
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
            var medicalDepartment = new MedicalDepartment
            {
                CreatedAt = DateTime.Now,
                Description = model.Description,
                Title = model.Title,
                PhotoName = await _fileService.UploadAsync(model.Photo),
            };

            await _medicalDepartmentRepository.CreateAsync(medicalDepartment);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteDepartment = await _medicalDepartmentRepository.GetAsync(id);
            await _medicalDepartmentRepository.DeleteAsync(deleteDepartment);
        }

        public async Task<MedicalDepartmentIndexVM> GetAllAsync()
        {
            var model = new MedicalDepartmentIndexVM
            {
                MedicalDepartments = await _medicalDepartmentRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<MedicalDepartmentUpdateVM> GetUpdateModelAsync(int id)
        {
            var updatemodel = await _medicalDepartmentRepository.GetAsync(id);

            var model = new MedicalDepartmentUpdateVM
            {
                Id = updatemodel.Id,
                Title = updatemodel.Title,
                Description = updatemodel.Description

            };
            return model;
        }

        public async Task<bool> UpdateAsync(MedicalDepartmentUpdateVM model)
        {
            var dbMedicalDepartment = await _medicalDepartmentRepository.GetAsync(model.Id);
            if (!_modelState.IsValid) return false;
            var isExist = await _medicalDepartmentRepository.AnyAsync(md => md.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && md.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This title already exist");
                return false;
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
                _fileService.Delete(dbMedicalDepartment.PhotoName);
                dbMedicalDepartment.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            dbMedicalDepartment.Title = model.Title;
            dbMedicalDepartment.Description = model.Description;
            dbMedicalDepartment.ModifiedAt = DateTime.Now;
            dbMedicalDepartment.ModifiedAt = DateTime.Now;

            await _medicalDepartmentRepository.UpdateAsync(dbMedicalDepartment);
            return true;
        }
    }
}
