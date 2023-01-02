using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Identity;

public class Address
{
    public int Id { get; set; }
    public string Country { get; set; } = null!;
    public string Province { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    
    [Required]
    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
}