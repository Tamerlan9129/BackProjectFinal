using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.Repositories.Concrete
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context) : base(context)
        {
           _context = context;
        }

        public async Task<IQueryable<Doctor>> FilterByName(string? name)
        {
            return _context.Doctors.Where(d => !string.IsNullOrEmpty(name) ? d.FullName.Contains(name) : true);
        }

        public async Task<List<Doctor>> GetHomeDoctorsAsync()
        {
            var doctors = await _context.Doctors.Where(d => d.ShowInHome).ToListAsync();
            return doctors;
        }

        public async Task<int> GetPageCountAsync(IQueryable<Doctor> doctors, int take)
        {
            var pageCount = await doctors.CountAsync();
            return (int)Math.Ceiling((decimal)pageCount / take);
        }

        public async Task<IQueryable<Doctor>> PaginateDoctorAsync(IQueryable<Doctor> doctors, int page, int take)
        {
            doctors = doctors.Skip((page - 1) * take).Take(take);
            return doctors;
        }
    }
}
