using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models
{
    public static class ModelBuilderExtention
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "649fca11-51e3-4db8-91d7-0bba63163280",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "35403dae-8769-4773-b617-a5087d827284",
                    Name = "User",
                    NormalizedName = "USER"
                }
                );

            modelBuilder.Entity<User>().HasData(
               new User
               {
                   Id = "422b7d11-6c54-436e-a04f-7619e7acf637",
                   Name = "Admin",
                   UserName = "admin@gmail.com",
                   NormalizedUserName = "ADMIN@GMAIL.COM",
                   Email = "admin@gmail.com",
                   NormalizedEmail = "ADMIN@GMAIL.COM",
                   PasswordHash = passwordHasher.HashPassword(null, "Admin@123"),
                   LockoutEnabled = true
               },
               new User
               {
                   Id = "5eedf264-7629-412a-937e-926ec371be3c",
                   Name = "User",
                   UserName = "USER@gmail.com",
                   NormalizedUserName = "USER@GMAIL.COM",
                   Email = "User@gmail.com",
                   NormalizedEmail = "USER@GMAIL.COM",
                   PasswordHash = passwordHasher.HashPassword(null, "USER@123"),
                   LockoutEnabled = true
               }
               );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "649fca11-51e3-4db8-91d7-0bba63163280",
                UserId = "422b7d11-6c54-436e-a04f-7619e7acf637"
            },
            new IdentityUserRole<string>
            {
                RoleId = "35403dae-8769-4773-b617-a5087d827284",
                UserId = "5eedf264-7629-412a-937e-926ec371be3c"
            }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Mobiles",
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    CreatedBy = "Admin",
                    UpdatedBy = "Admin"
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Clothing",
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    CreatedBy = "Admin",
                    UpdatedBy = "Admin"
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Grooming",
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    CreatedBy = "Admin",
                    UpdatedBy = "Admin"
                }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    ProductName = "IPhone",
                    Price = 79999,
                    Description = "This is an IPhone.",
                    Image = "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/refurb-iphone-13-pro-max-blue-2023?wid=1144&hei=1144&fmt=jpeg&qlt=90&.v=1679072989205",
                    Quantity = 10,
                    CategoryId = 1,
                    
                },
                 new Product
                 {
                     ProductId = 2,
                     ProductName = "Shoes",
                     Price = 5999,
                     Description = "This is Nike Shoe.",
                     Image = "https://www.jagranimages.com/images/newimg/26122022/26_12_2022-best_nike_shoes_in_india_23272173.jpg",
                     Quantity = 10,
                     CategoryId = 2,

                 },
                  new Product
                  {
                      ProductId = 3,
                      ProductName = "Perfume",
                      Price = 699,
                      Description = "This is a Perfume.",
                      Image = "https://www.wildstone.in/cdn/shop/files/ultra100ml_1_grande.jpg?v=1695879969",
                      CategoryId = 3,

                  }
            );
        }
    }
}
