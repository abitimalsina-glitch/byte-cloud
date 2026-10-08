using ByteCloud.Data;
using Users.Models;
using Login.DTO;
using Register.DTO;
using Microsoft.AspNetCore.Mvc;
using PasswordService.Services;
using Microsoft.AspNetCore.RateLimiting;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private ByteCloudDbContext context;
    private PasswordServices passwordServices;

    // ASP.NET Core automatically provides these dependencies
    // through Dependency Injection when the controller is created.
    public AuthController(
        ByteCloudDbContext context,
        PasswordServices passwordServices)
    {
        this.context = context;
        this.passwordServices = passwordServices;
    }
    
    [EnableRateLimiting("login")]
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        // Search the database for a user with the email
        // provided in the login request.
        User? user = context.Users.FirstOrDefault(
            user => user.Email == request.Email
        );

        // If no user was found, return a generic error.
        // We use the same message as an incorrect password
        // so attackers cannot determine whether an email exists.
        if (user == null)
        {
            return Unauthorized("Email or Password is incorrect");
        }

        // Verify the password provided by the user
        // against the password hash stored in the database.
        bool passwordCorrect = passwordServices.VerifyPassword(
            user,
            request.Password,
            user.PasswordHash
        );

        // If the password does not match the stored hash,
        // authentication fails.
        if (!passwordCorrect)
        {
            return Unauthorized("Email or Password is incorrect");
        }

        // The credentials are correct.
        // return the username.
        // Later, this will be replaced with an authentication
        // mechanism such as a cookie or JWT.
        return Ok(user.Username);
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        // Check whether the requested email is already
        // associated with an existing account.
        User? existingUser = context.Users.FirstOrDefault(
            user => user.Email == request.Email
        );

        // Prevent multiple accounts from using the same email.
        if (existingUser != null)
        {
            return Conflict("Email Already Registered");
        }

        // Check whether the requested username is already
        // being used by another account.
        User? existingUsername = context.Users.FirstOrDefault(
            user => user.Username == request.Username
        );

        // Prevent duplicate usernames.
        if (existingUsername != null)
        {
            return Conflict("Username Already Taken");
        }

        // Create a new User object.
        // The password is hashed before it is stored.
        User user = new User
        {
            Email = request.Email,
            Username = request.Username,

            // Never store the user's plain-text password.
            PasswordHash = passwordServices.HashPassword(
                new User(),
                request.Password
            ),

            // Record when the account was created.
            CreatedAt = DateTime.UtcNow,

            // Give the new user a 10 GB storage limit.
            StorageLimit = 10737418240,

            // New users start with no stored files.
            StorageUsed = 0
        };

        // Add the new user to EF Core's change tracker.
        context.Users.Add(user);

        // Save the new user to the database.
        context.SaveChanges();

        // Return a successful HTTP response.
        return Ok("User Registered Successfully");
    }
}