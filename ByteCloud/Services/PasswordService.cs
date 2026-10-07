using Users.Models;
using Microsoft.AspNetCore.Identity;

namespace PasswordService.Services;

public class PasswordServices
{
    private PasswordHasher<User> hasher = new PasswordHasher<User>();

    public string HashPassword(User user, string password)
    {
        return hasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string password, string passwordHash)
    {
        PasswordVerificationResult result =
            hasher.VerifyHashedPassword(user, passwordHash, password);

        return result == PasswordVerificationResult.Success;
    }
}