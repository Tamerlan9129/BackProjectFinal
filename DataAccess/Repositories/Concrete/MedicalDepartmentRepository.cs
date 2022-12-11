using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class MedicalDepartmentRepository : Repository<MedicalDepartment>, IMedicalDepartmentRepository
    {
        public MedicalDepartmentRepository(AppDbContext context):base(context)
        {

        }
    }
}
