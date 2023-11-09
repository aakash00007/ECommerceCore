using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models
{
    public class Category
    {
        
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set;}
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set;}
        public virtual ICollection<Product>? Products { get; set; }
    }
}
