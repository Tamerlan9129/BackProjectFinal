using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class BasketRepository : Repository<Basket>, IBasketRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketRepository(AppDbContext context,UserManager<User> userManager,IHttpContextAccessor httpContextAccessor): base(context)
        {
            _context = context;
            _userManager = userManager;
           _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Basket> GetBasket(string userId)
        {
            var basket = await _context.Baskets
             .Include(b => b.BasketProducts)
             .ThenInclude(bp => bp.Product)
             .FirstOrDefaultAsync(b => b.UserId == userId);
            return basket;
        }
    }
}
