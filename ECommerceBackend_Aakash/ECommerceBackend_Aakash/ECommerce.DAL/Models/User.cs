using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
