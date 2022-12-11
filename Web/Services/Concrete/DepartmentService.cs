using DataAccess.Repositories.Abstract;
using Web.Services.Abstract;
using Web.ViewModels.Department;

namespace Web.Services.Concrete
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMedicalDepartmentRepository _medicalDepartmentRepository;

        public DepartmentService(IMedicalDepartmentRepository medicalDepartmentRepository)
        {
            _medicalDepartmentRepository = medicalDepartmentRepository;
        }

        public async Task<DepartmentIndexVM> GetAllAsync()
        {
            var model = new DepartmentIndexVM
            {
                MedicalDepartments = await _medicalDepartmentRepository.GetAllAsync(),
            };
            return model;
        }
    }
}
