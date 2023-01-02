using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class AddressDto
{
    [Required] public string Country { get; set; } = null!;
    [Required] public string Province { get; set; } = null!;
    [Required] public string PhoneNumber { get; set; } = null!;
}