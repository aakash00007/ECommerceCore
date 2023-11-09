using System.ComponentModel.DataAnnotations;

namespace ECommerceFrontend_Aakash.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string? UserId { get; set; }
        public virtual User? User { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

    }
}
