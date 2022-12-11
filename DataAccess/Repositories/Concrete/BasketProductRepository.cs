using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class BasketProductRepository : Repository<BasketProduct>, IBasketProductRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public BasketProductRepository(AppDbContext context,UserManager<User> userManager):base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> DeleteProductAsync(int productdId)
        {
            var basketProduct = await _context.BasketProducts.FirstOrDefaultAsync(p => p.ProductId == productdId);
            if (basketProduct == null)
            {
                return false;
            }
            _context.BasketProducts.Remove(basketProduct);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BasketProduct> GetBasketProducts(int modelId, int basketId)
        {
            var basketProduct = await _context.BasketProducts.FirstOrDefaultAsync(bp => bp.ProductId == modelId && bp.BasketId == basketId);

            return basketProduct;
        }

        public async Task<int> GetUserBasketProductsCount(ClaimsPrincipal userClaims)
        {
            var user = await _userManager.GetUserAsync(userClaims);
            if (user == null) return 0;
            var basketProductCount = await _context.BasketProducts.Where(bp => bp.Basket.UserId == user.Id).SumAsync(bp => bp.Quantity);
            return basketProductCount;
        }
    }
}
