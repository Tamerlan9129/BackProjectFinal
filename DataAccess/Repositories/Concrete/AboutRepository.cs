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
    public class AboutRepository: Repository<About>, IAboutRepository
    {
        private readonly AppDbContext _context;

        public AboutRepository(AppDbContext context) : base(context)
        {
           _context = context;
        }
    }
}
