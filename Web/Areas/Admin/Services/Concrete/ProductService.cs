using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Product;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public ProductService(IProductRepository productRepository,IProductCategoryRepository productCategoryRepository,
            IFileService fileService,IActionContextAccessor actionContextAccessor)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(ProductCreateVM model)
        {
            model.Categories = await GetCategoriesAsync();
            if (!_modelState.IsValid) return false;
            var isExist = await _productRepository.AnyAsync(p => p.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This title already exist");
                return false;
            }
            int maxSize = 1500;
            if (!_fileService.CheckPhoto(model.Photo))
            {
                _modelState.AddModelError("Photo", "The photo must be in image format");
                return false;
            }
            else if (!_fileService.MaxSize(model.Photo, maxSize))
            {
                _modelState.AddModelError("Photo", $"The photo size over than {maxSize} kb");
                return false;
            }
            var product = new Product
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
                CategoryId = model.CategoryId,
                Quantity = model.Quantity,
                Price = model.Price,
                Photoname = await _fileService.UploadAsync(model.Photo)
            };

            await _productRepository.CreateAsync(product);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);
            _fileService.Delete(product.Photoname);
            await _productRepository.DeleteAsync(product);
        }

        public async Task<ProductIndexVM> GetAllAsync()
        {
            var model = new ProductIndexVM
            {
                Products = await _productRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<List<SelectListItem>> GetCategoriesAsync()
        {
            var categories = await _productCategoryRepository.GetAllAsync();
            return categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();
        }

        public async Task<ProductCreateVM> GetCategoriesCreateModelAsync()
        {
            var categories = await _productCategoryRepository.GetAllAsync();
            var model = new ProductCreateVM
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList()
            };
            return model;
        }

        public  async Task<ProductIndexVM> GetProductsWithCategoryAsync()
        {
            var model = new ProductIndexVM
            {
                Products = await _productRepository.GetProductsWithCategoryAsync()
            };
            return model;
        }

        public async Task<ProductUpdateVM> GetUpdateModelAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);
            var model = new ProductUpdateVM
            {
                Id = product.Id,
                Title = product.Title,
                CategoryId = product.CategoryId,
                Categories = await GetCategoriesAsync(),
                Price = product.Price,
                Quantity = product.Quantity
            };
            return model;
        }

        public async Task<bool> UpdateAsync(ProductUpdateVM model)
        {
            model.Categories = await GetCategoriesAsync();
            if (!_modelState.IsValid) return false;
            var isExist = await _productRepository.AnyAsync(p => p.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && model.Id != p.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This title already exist");
                return false;
            }
            var product = await _productRepository.GetAsync(model.Id);
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.ModifiedAt = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;


            if (model.Photo != null)
            {
                int maxSize = 1500;
                if (!_fileService.CheckPhoto(model.Photo))
                {
                    _modelState.AddModelError("Photo", "The photo must be in image format");
                    return false;
                }
                else if (!_fileService.MaxSize(model.Photo, maxSize))
                {
                    _modelState.AddModelError("Photo", $"The photo size over than {maxSize} kb");
                    return false;
                }
                _fileService.Delete(product.Photoname);
                product.Photoname = await _fileService.UploadAsync(model.Photo);
            }
            await _productRepository.UpdateAsync(product);
            return true;
        }
    }
}
