using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models.DTOModels
{
    public class ProductDTO
    {
        
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Image { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string? UserId { get; set; }
        public int? CategoryId { get; set; }
    }
}
