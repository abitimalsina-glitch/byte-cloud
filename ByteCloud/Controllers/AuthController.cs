using ByteCloud.Data;
using Users.Models;
using Login.DTO;
using Register.DTO;
using Microsoft.AspNetCore.Mvc;
using PasswordService.Services;
using Normalizer.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Email.Services;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> userManager;
    private readonly EmailService emailService;
    private readonly ByteCloudDbContext context;
    private readonly PasswordServices passwordServices;

    // ASP.NET Core automatically provides these dependencies
    // through Dependency Injection when the controller is created.
    public AuthController(
        ByteCloudDbContext context,
        PasswordServices passwordServices,
        UserManager<User> userManager,
        EmailService emailService)
    {
        this.context = context;
        this.passwordServices = passwordServices;
        this.userManager = userManager;
        this.emailService = emailService;
    }

    [EnableRateLimiting("login")]
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        // Normalize the email so "Bob@X.com " matches the stored "bob@x.com".
        string email = AccountNormalizer.NormalizeEmail(request.Email);

        // Search the database for a user with the normalized email.
        User? user = context.Users.FirstOrDefault(
            user => user.Email == email
        );

        // If no user was found, return a generic error.
        // We use the same message as an incorrect password
        // so attackers cannot determine whether an email exists.
        if (user == null)
        {
            return Unauthorized("Email or Password is incorrect");
        }

        if (user.PasswordHash == null)
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
        // Return the username.
        // Later, this will be replaced with an authentication
        // mechanism such as a cookie.
        return Ok(user.UserName);
    }

    [EnableRateLimiting("register")]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        // Normalize once, then use the same value for the
        // duplicate check and for what gets stored.
        string email = AccountNormalizer.NormalizeEmail(request.Email);

        // Check whether the requested email is already associated
        // with an existing account.
        User? existingUser = await userManager.FindByEmailAsync(email);

        if (existingUser != null)
        {
            return Conflict("Email Already Registered");
        }

        // Check whether the requested username is already being used.
        User? existingUsername = await userManager.FindByNameAsync(
            request.Username
        );

        if (existingUsername != null)
        {
            return Conflict("Username Already Taken");
        }

        // Create the user. Identity handles password hashing.
        User user = new User
        {
            Email = email,
            UserName = request.Username,
            CreatedAt = DateTime.UtcNow
        };

        IdentityResult result = await userManager.CreateAsync(
            user,
            request.Password
        );

        if (!result.Succeeded)
        {
            // Handle a possible duplicate or another Identity validation error.
            if (result.Errors.Any(error =>
                error.Code.Contains(
                    "Duplicate",
                    StringComparison.OrdinalIgnoreCase)))
            {
                return Conflict("Email or username already taken");
            }

            return BadRequest(
                result.Errors.Select(error => error.Description)
            );
        }

        return Ok("User Registered Successfully");
    }
}