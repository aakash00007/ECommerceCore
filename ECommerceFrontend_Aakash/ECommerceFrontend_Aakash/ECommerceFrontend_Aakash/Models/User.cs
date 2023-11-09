using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerceFrontend_Aakash.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; }
        public virtual ICollection<Product>? Service { get; set; }
    }
}
