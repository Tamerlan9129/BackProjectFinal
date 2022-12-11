using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.ViewModels.Shop;

namespace Web.Services.Concrete
{
    public class ShopService : IShopService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ShopService(IProductRepository productRepository,IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<ShopIndexVM> GetAllProductsWithCategoriesAsync(ShopIndexVM model)
        {
            var products = await _productRepository.FilterByName(model.Name);
            products = await _productRepository.FilterByCategoryIdAsync(products, model.CategoryId);
            var pageCount = await _productRepository.GetPageCountAsync(products, model.Take);
            products = await _productRepository.PaginateProductAsync(products, model.Page, model.Take);

            model = new ShopIndexVM
            {
                Products = products.ToList(),
                Categories = await _productCategoryRepository.GetCategoryWithProduct(),
                PageCount = pageCount,
                Page = model.Page,
                Name = model.Name,
                Take = model.Take
            };
            return model;
        }

        public async Task<int> GetPageCountAsync(IQueryable<Doctor> doctors, int take)
        {
            var pageCount = await doctors.CountAsync();
            return (int)Math.Ceiling((decimal)pageCount / take);
        }
    }
}
