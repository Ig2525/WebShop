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

            }
        }
    }
}
