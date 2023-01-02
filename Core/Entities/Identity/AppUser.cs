using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity;

public class AppUser: IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Address Address { get; set; }  = null!;
}