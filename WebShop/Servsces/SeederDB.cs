using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Data.Entities;
using WebShop.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using WebShop.Constants;

namespace WebShop.Servsces
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MyAppContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
                
                context.Database.Migrate();
                
                if (!context.Categories.Any())
                {
                    string[] categories = { "Ноутбуки", "Монітори", "Одяг" };
                    foreach (var name in categories)
                    {
                        CategoryEntity cat = new CategoryEntity
                        {
                            Name = name
                        };
                        context.Categories.Add(cat);
                        context.SaveChanges();
                    }
                }

                if (!roleManager.Roles.Any())
                {
                    RoleEntity admin = new RoleEntity
                    {
                        Name = Roles.Admin
                    };
                    var result = roleManager.CreateAsync(admin).Result;
                    RoleEntity user = new RoleEntity
                    {
                        Name = Roles.User
                    };
                    result = roleManager.CreateAsync(user).Result;
                }

                if (!userManager.Users.Any())
                {
                    var user = new UserEntity
                    {
                        Email = "admin@gmail.com",
                        UserName= "admin@gmail.com",
                        PhoneNumber= "098 34 23 211"
                    };
                    var result = userManager.CreateAsync(user,"123456").Result;
                    
                    if (result.Succeeded)
                    {
                        result = userManager.AddToRoleAsync(user, Roles.Admin).Result;
                    }
                }

                if (!context.Products.Any())
                {
                    ProductEntity product = new ProductEntity
                    {
                        Name = "Skechers D'Lites Черевики",
                        CategoryId = 3,
                        Price = 3199,
                        Description = "Черевики чоловічі Skechers D'Lites (999304 BKW) чорного кольору. " +
                        "Стильні черевики з м'якою устілкою, на високій пружній підошві."
                    };
                    context.Products.Add(product);
                    context.SaveChanges();
                    for (int i = 1; i <= 6; i++)
                    {
                        var image = new ProductImageEntity
                        {
                            Name = $"{i}p.jpg",
                            Priority = i,
                            ProductId = product.Id
                        };
                        context.ProductImages.Add(image);
                        context.SaveChanges();
                    }
                }


            }
        }
    }
}
