using ECommerce.BAL.Repository.Interfaces;
using ECommerce.DAL.DBContext;
using ECommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BAL.Repository
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDBContext context) : base(context) { }
        public IEnumerable<Cart> GetCartByUser(string userId)
        {
            return _context.Carts.Where(c => c.UserId == userId);
        }
    }
}
