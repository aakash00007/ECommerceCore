using ECommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BAL.Repository.Interfaces
{
    public interface IProductRepository
    {
        Tuple<IEnumerable<Product>, int> GetDataByPagination(int pageNo, string search);
    }
}
