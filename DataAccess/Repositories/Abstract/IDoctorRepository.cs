using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<IQueryable<Doctor>> PaginateDoctorAsync(IQueryable<Doctor> doctors, int page, int take);
        Task<int> GetPageCountAsync(IQueryable<Doctor> doctors, int take);
        Task<IQueryable<Doctor>> FilterByName(string? name);
        Task<List<Doctor>> GetHomeDoctorsAsync();
    }
}
