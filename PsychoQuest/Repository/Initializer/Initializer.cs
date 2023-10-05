using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Repository.Initializer;

public class Initializer
{
    public static async Task InitializerRoleAsync(RoleManager<IdentityRole<long>> roleManager)
    {
        if (await roleManager.FindByNameAsync("Admin") == null)
        {
            await roleManager.CreateAsync(new IdentityRole<long>("Admin"));
        }
        if (await roleManager.FindByNameAsync("User") == null)
        {
            await roleManager.CreateAsync(new IdentityRole<long>("User"));
        }
    }
    
    public static async Task InitializerUserAsync(UserManager<User> userManager)
    {
        var adminUser = new
        {
            UserName = "Admin",
            Email = "admin@gmail.com",
            Password = "1234"
        };
        var firstUser = new
        {
            UserName = "user",
            Email = "user@gmail.com",
            Password = "1111"
        };
        
        var userFirst = await userManager.FindByEmailAsync(firstUser.Email);
        var userAdmin = await userManager.FindByEmailAsync(adminUser.Email);
        
        if (userAdmin is null)
        {
            var user = new User()
            {
                UserName = adminUser.UserName,
                Email = adminUser.Email
            };
            await userManager.CreateAsync(user, adminUser.Password);
            await userManager.AddToRoleAsync(user, "Admin");
        }
        if (userFirst is null)
        {
            var user = new User()
            {
                UserName = firstUser.UserName,
                Email = firstUser.Email
            };
            await userManager.CreateAsync(user, firstUser.Password);
            await userManager.AddToRoleAsync(user, "User");
        }
    }
}