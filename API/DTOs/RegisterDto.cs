using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required] public string FirstName { get; set; } = null!;
    [Required] public string SecondName { get; set; } = null!;
    [Required] public string LastName { get; set; } = null!;
    [Required] [EmailAddress] public string Email { get; set; } = null!;

    [Required]
    [RegularExpression(
        "(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
        ErrorMessage = "Password Requirements wrong ! 1 1 1 1 ")]
    public string Password { get; set; } = null!;
}