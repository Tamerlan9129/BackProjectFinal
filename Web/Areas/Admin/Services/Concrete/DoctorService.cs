using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Doctor;

namespace Web.Areas.Admin.Services.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly IFileService _fileService;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ModelStateDictionary _modelState;

        public DoctorService(IFileService fileService, IDoctorRepository doctorRepository, IActionContextAccessor actionContextAccessor)
        {
            _fileService = fileService;
            _doctorRepository = doctorRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(DoctorCreateVM model, List<string> SkillsList)
        {
            if (!_modelState.IsValid) return false;
            int maxSize = 1500;
            if (!_fileService.CheckPhoto(model.Photo))
            {
                _modelState.AddModelError("Photo", "The photo must be in image format");
                return false;
            }
            else if (!_fileService.MaxSize(model.Photo, maxSize))
            {
                _modelState.AddModelError("Photo", $"The photo size over than {maxSize} kb");
                return false;
            }

            string skills = string.Join(",", SkillsList);

            var doctor = new Doctor
            {
                //ShowInHome = model.ShowInHome,
                CreatedAt = DateTime.Now,
                Twitter = model.Twitter,
                Facebook = model.Facebook,
                LinkedIn = model.LinkedIn,
                Phone = model.Phone,
                Position = model.Position,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                MondayToFridayEnd = model.MondayToFridayEnd,
                MondayToFridayStart = model.MondayToFridayStart,
                SaturdayEnd = model.SaturdayEnd,
                SaturdayStart = model.SaturdayStart,
                Skills = skills,
                Email = model.Email,
                FullName = model.FullName,
                Info = model.Info,
                Introducing = model.Introducing,
                SundayIsWorking = model.SundayIsWorking,
                Qualification = model.Qualification
            };
            await _doctorRepository.CreateAsync(doctor);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            _fileService.Delete(doctor.PhotoName);
            await _doctorRepository.DeleteAsync(doctor);
        }

        public async Task<DoctorIndexVM> GetAllAsync()
        {
            var model = new DoctorIndexVM
            {
                Doctors = await _doctorRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<DoctorDetailsVM> GetDetailsModelAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            var model = new DoctorDetailsVM
            {
                Id = doctor.Id,
                 //ShowInHome = doctor.ShowInHome,
                CreatedAt = doctor.CreatedAt,
                Twitter = doctor.Twitter,
                Facebook = doctor.Facebook,
                LinkedIn = doctor.LinkedIn,
                Phone = doctor.Phone,
                Position = doctor.Position,
                PhotoName = doctor.PhotoName,
                MondayToFridayEnd = doctor.MondayToFridayEnd,
                MondayToFridayStart = doctor.MondayToFridayStart,
                SaturdayEnd = doctor.SaturdayEnd,
                SaturdayStart = doctor.SaturdayStart,
                Skills = doctor.Skills,
                Email = doctor.Email,
                FullName = doctor.FullName,
                Info = doctor.Info,
                Introducing = doctor.Introducing,
                SundayIsWorking = doctor.SundayIsWorking,
                Qualification = doctor.Qualification,
                ModifiedAt = doctor.ModifiedAt
            };

            return model;
        }

        public async Task<DoctorUpdateVM> GetUpdateModelAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            var model = new DoctorUpdateVM
            {
                Id = doctor.Id,
               //ShowInHome = doctor.ShowInHome,
                Twitter = doctor.Twitter,
                Facebook = doctor.Facebook,
                LinkedIn = doctor.LinkedIn,
                Phone = doctor.Phone,
                Position = doctor.Position,
                MondayToFridayEnd = doctor.MondayToFridayEnd,
                MondayToFridayStart = doctor.MondayToFridayStart,
                SaturdayEnd = doctor.SaturdayEnd,
                SaturdayStart = doctor.SaturdayStart,
                Skills = doctor.Skills,
                Email = doctor.Email,
                FullName = doctor.FullName,
                Info = doctor.Info,
                Introducing = doctor.Introducing,
                SundayIsWorking = doctor.SundayIsWorking,
                Qualification = doctor.Qualification,

            };
            return model;
        }

        public async Task<bool> UpdateAsync(DoctorUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var doctor = await _doctorRepository.GetAsync(model.Id);
            doctor.Qualification = model.Qualification;
            doctor.Position = model.Position;
            doctor.SaturdayStart = model.SaturdayStart;
            doctor.SaturdayEnd = model.SaturdayEnd;
            doctor.MondayToFridayStart = model.MondayToFridayStart;
            doctor.MondayToFridayEnd = model.MondayToFridayEnd;
            //doctor.ShowInHome = model.ShowInHome;
            doctor.Twitter = model.Twitter;
            doctor.Facebook = model.Facebook;
            doctor.LinkedIn = model.LinkedIn;
            doctor.Skills = model.Skills;
            doctor.FullName = model.FullName;
            doctor.Phone = model.Phone;
            doctor.Info = model.Info;
            doctor.Introducing = model.Introducing;
            doctor.SundayIsWorking = model.SundayIsWorking;
            doctor.ModifiedAt = DateTime.Now;
            doctor.Email = model.Email;

           
           
           
            if (model.Photo != null)
            {
                int maxSize = 1500;
                if (!_fileService.CheckPhoto(model.Photo))
                {
                    _modelState.AddModelError("Photo", "The photo must be in image format");
                    return false;
                }
                else if (!_fileService.MaxSize(model.Photo, maxSize))
                {
                    _modelState.AddModelError("Photo", $"The photo size over than {maxSize}kb");
                    return false;
                }
                _fileService.Delete(doctor.PhotoName);
                doctor.PhotoName = await _fileService.UploadAsync(model.Photo);
            }

            await _doctorRepository.UpdateAsync(doctor);
            return true;


        }
    }
}
