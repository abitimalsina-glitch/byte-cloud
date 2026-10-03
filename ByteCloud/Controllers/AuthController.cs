using ByteCloud.Data;
using ByteCloud.Models;
using ByteCloud.DTO;
using Microsoft.AspNetCore.Mvc;
using PasswordService.Services;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private ByteCloudDbContext context;
    private PasswordServices passwordServices;
    public AuthController(ByteCloudDbContext context, PasswordServices passwordServices)
    {
        this.context = context;
        this.passwordServices = passwordServices;
    }

    [HttpPost("login")]
    public string Login(LoginRequest request)
    {
        User? user = context.Users.FirstOrDefault(user => user.Email == request.Email);

        if (user == null)
        {
            return "User not found";
        }

        bool passwordCorrect = passwordServices.VerifyPassword(user, request.Password, user.PasswordHash);

        if (!passwordCorrect)
        {
            return "Invalid Password";
        }

        return user.Username;
    }
}