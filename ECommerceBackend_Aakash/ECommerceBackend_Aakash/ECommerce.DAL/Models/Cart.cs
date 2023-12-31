﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Product? Product { get; set; }
        public string? UserId { get; set;}
        public virtual User? User { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
