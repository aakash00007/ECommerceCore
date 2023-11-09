using ECommerce.BAL.Repository.Interfaces;
using ECommerce.DAL.DBContext;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BAL.Repository
{
    public class ProductRepository : GenericRepository<Product> ,IProductRepository
    {
        public ProductRepository(AppDBContext appDBContext) : base(appDBContext) { }
        public Tuple<IEnumerable<Product>, int> GetDataByPagination(int pageNo, string search = "")
        {
            int pageSize = 5;
            int totalRecords = _context.Products.Where(x => (x.ProductName.ToLower().StartsWith(search== null ? "" : search.ToLower()) || x.Category.CategoryName.ToLower().Contains(search == null ? "" : search.ToLower()))).Count();
            int totalPages = 0;
            totalPages = totalRecords / pageSize;
            if (totalRecords % pageSize != 0)
            {
                totalPages += 1;
            }

            Tuple<IEnumerable<Product>, int> data = new Tuple<IEnumerable<Product>, int>(_context.Products.Where(x => (x.ProductName.ToLower().StartsWith(search == null ? "" : search.ToLower()) || x.Category.CategoryName.ToLower().Contains(search == null ? "" : search.ToLower()))).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList(), totalPages);
            return data;
        }
    }
}
