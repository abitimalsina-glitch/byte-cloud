using System.ComponentModel.DataAnnotations;
namespace Register.DTO;

public class RegisterRequest
{   
    [Required]
    [EmailAddress (ErrorMessage="Enter a valid Email")]
    [StringLength(254, ErrorMessage ="The email is too long")]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [StringLength(128, MinimumLength = 8,
    ErrorMessage="Password must be between 8 and 128 characters")]
    [RegularExpression(@"\A(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9\s]).{8,128}\z",
    ErrorMessage = "Password must contain uppercase, lowercase, number, and special character")]
    public string Password {get; set;} = string.Empty;

    [Required]
    [StringLength(20, MinimumLength = 3, ErrorMessage ="Username must be between 3 and 20 characters")]
    [RegularExpression(@"^[a-zA-Z0-9_]+$",
    ErrorMessage = "Username can only contain letters, numbers, and underscores")]
    public string Username {get; set;} = string.Empty;
}