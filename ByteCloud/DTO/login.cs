using System.ComponentModel.DataAnnotations;

namespace Login.DTO;

public class LoginRequest
{
    [Required]
    [EmailAddress(ErrorMessage = "Enter a valid email")]
    [StringLength(254, ErrorMessage = "The email is too long")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(
        128,
        ErrorMessage = "Password cannot exceed 128 characters")]
    public string Password { get; set; } = string.Empty;
}
