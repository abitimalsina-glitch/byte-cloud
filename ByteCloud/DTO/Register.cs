using System.ComponentModel.DataAnnotations;
namespace Register.DTO;

public class RegisterRequest
{   
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [StringLength(25, MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+$",
    ErrorMessage = "Password must contain uppercase, lowercase, number, and special character.")]
    public string Password {get; set;} = string.Empty;

    [Required]
    [StringLength(15, MinimumLength = 3)]
    [RegularExpression(@"^[a-zA-Z0-9_]+$",
    ErrorMessage = "Username can only contain letters, numbers, and underscores")]
    public string Username {get; set;} = string.Empty;
}