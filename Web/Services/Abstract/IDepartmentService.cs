using Web.ViewModels.Department;

namespace Web.Services.Abstract
{
    public interface IDepartmentService
    {
        Task<DepartmentIndexVM> GetAllAsync();
    }
}
