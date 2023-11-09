using ECommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BAL.Repository.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        IEnumerable<Cart> GetCartByUser(string userId);
    }
}
