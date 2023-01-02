using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class AppIdentityDbContextSeed
{
    
    public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                FirstName = "admin",
                SecondName = "admin",
                LastName = "admin",
                Email = "admin@admin.com",
                UserName = "admin",
                Address = new Address
                {
                    Country = "Iraq",
                    Province = "Baghdad",
                    PhoneNumber = "07733810890"
                }
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}