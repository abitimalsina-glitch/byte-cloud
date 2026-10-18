using Microsoft.AspNetCore.Identity;

namespace Users.Models;

public class User : IdentityUser<int>
{
    public string? ProfilePicture { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public long? StorageLimit { get; set; } = null;

    public long? StorageUsed { get; set; } = null;

    public bool IsEmailVerified { get; set; } = false;
}