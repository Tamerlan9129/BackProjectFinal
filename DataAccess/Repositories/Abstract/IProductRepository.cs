using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategoryAsync();
        Task<IQueryable<Product>> FilterByCategoryIdAsync(IQueryable<Product> products, int? categoryId);
        Task<IQueryable<Product>> FilterByName(string? name);
        Task<IQueryable<Product>> PaginateProductAsync(IQueryable<Product> products, int page, int take);
        Task<int> GetPageCountAsync(IQueryable<Product> products, int take);
    }
}
