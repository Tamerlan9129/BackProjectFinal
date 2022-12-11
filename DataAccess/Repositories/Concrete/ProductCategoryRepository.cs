using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        private readonly AppDbContext _context;

        public ProductCategoryRepository(AppDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<List<ProductCategory>> GetCategoryWithProduct()
        {
            var categories = await _context.ProductCategories.Include(p => p.Products).ToListAsync();
            return categories;
        }
    }
}
