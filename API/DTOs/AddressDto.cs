using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class AddressDto
{
    [Required] public string FirstName { get; set; } = null!;
    [Required] public string LastName { get; set; } = null!;
    [Required] public string Street { get; set; } = null!;
    [Required] public string City { get; set; } = null!;
    [Required] public string State { get; set; } = null!;
    [Required] public string ZipCode { get; set; } = null!;
}