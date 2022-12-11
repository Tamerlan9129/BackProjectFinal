using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Web.Services.Abstract;
using Web.ViewModels.Basket;

namespace Web.Services.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketProductRepository _basketProductRepository;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketService(IBasketRepository basketRepository,IBasketProductRepository basketProductRepository,
            IProductRepository productRepository,UserManager<User> userManager,IHttpContextAccessor httpContextAccessor)
        {
            _basketRepository = basketRepository;
            _basketProductRepository = basketProductRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Add(int productId)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return false;

            var product = await _productRepository.GetAsync(productId);
            if (product == null) return false;

            var basket = await _basketRepository.GetBasket(user.Id);

            if (basket == null)
            {
                basket = new Basket
                {
                    UserId = user.Id,
                    CreatedAt = DateTime.Now
                };
                await _basketRepository.CreateAsync(basket);
            }

            var basketProduct = await _basketProductRepository.GetBasketProducts(productId, basket.Id);
            if (basketProduct != null)
            {
                basketProduct.Quantity++;
                await _basketProductRepository.UpdateAsync(basketProduct);
            }
            else
            {
                basketProduct = new BasketProduct
                {
                    Quantity = 1,
                    BasketId = basket.Id,
                    ProductId = product.Id,
                    CreatedAt = DateTime.Now
                };
                await _basketProductRepository.CreateAsync(basketProduct);
            }

            return true;
        }

        public async Task<bool> ClearBasket(int id)
        {
            var basket = await _basketRepository.GetAsync(id);
            if (basket == null) return false;
            await _basketRepository.DeleteAsync(basket);
            return true;
        }

        public async Task<bool> DecreaseCountAsync(int id)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return false;
            var basketProduct = await _basketRepository.GetBasket(user.Id);
            if (basketProduct != null)
            {
                foreach (var dbbasketProduct in basketProduct.BasketProducts)
                {
                    if (dbbasketProduct.ProductId == id)
                    {
                        if (dbbasketProduct.Quantity > 1)
                        {
                            dbbasketProduct.Quantity--;
                            await _basketRepository.UpdateAsync(basketProduct);
                        }
                    }
                }
            }
            return true;
        }

        public async Task<BasketIndexVM> GetBasketProducts()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return null;
            var basket = await _basketRepository.GetBasket(user.Id);
            var model = new BasketIndexVM();
            if (basket != null)
            {
                foreach (var basketProduct in basket.BasketProducts)
                {
                    var basketProducts = new BasketProductVM
                    {
                        Id = basketProduct.ProductId,
                        Title = basketProduct.Product.Title,
                        Quantity = basketProduct.Quantity,
                        Price = basketProduct.Product.Price,
                        Photoname = basketProduct.Product.Photoname
                    };
                    model.BasketId = basketProduct.BasketId;
                    model.BasketProducts.Add(basketProducts);
                }
            }
            else
            {
                basket = new Basket();
            }
            return model;
        }

        public async Task<bool> IncreaseCountAsync(int id)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return false;
            var basketProduct = await _basketRepository.GetBasket(user.Id);
            if (basketProduct != null)
            {
                foreach (var dbbasketProduct in basketProduct.BasketProducts)
                {
                    if (dbbasketProduct.ProductId == id)
                    {
                        dbbasketProduct.Quantity++;
                        await _basketRepository.UpdateAsync(basketProduct);
                    }
                }
            }
            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return false;

            var basketProduct = await _basketRepository.GetBasket(user.Id);
            if (basketProduct == null) return false;

            var product = await _productRepository.GetAsync(id);
            if (product == null) return false;

            await _basketProductRepository.DeleteProductAsync(product.Id);
            return true;
        }
    }
}
