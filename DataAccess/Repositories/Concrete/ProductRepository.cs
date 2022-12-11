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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context): base(context)
        {
           _context = context;
        }

        public async Task<IQueryable<Product>> FilterByCategoryIdAsync(IQueryable<Product> products, int? categoryId)
        {
            return products.Where(p => categoryId != 0 ? p.CategoryId == categoryId : true);
        }

        public async Task<IQueryable<Product>> FilterByName(string? name)
        {
            return _context.Products.Where(d => !string.IsNullOrEmpty(name) ? d.Title.Contains(name) : true);
        }

        public async Task<int> GetPageCountAsync(IQueryable<Product> products, int take)
        {
            var pageCount = await products.CountAsync();
            return (int)Math.Ceiling((decimal)pageCount / take);
        }

        public async Task<List<Product>> GetProductsWithCategoryAsync()
        {
            var products = await _context.Products.Include(ct => ct.Category).ToListAsync();

            return products;
        }

        public async Task<IQueryable<Product>> PaginateProductAsync(IQueryable<Product> products, int page, int take)
        {
            products = products
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * take)
                .Take(take);
            return products;
        }
    }
}
