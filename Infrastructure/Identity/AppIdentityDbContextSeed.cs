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
                DisplayName = "admin",
                Email = "admin@admin.com",
                UserName = "admin",
                Address = new Address
                {
                    FirstName = "admin",
                    LastName = "admin",
                    FullAddress = "baghdad",
                    Remarks = ""
                }
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}