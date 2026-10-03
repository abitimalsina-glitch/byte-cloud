using ByteCloud.Data;
using ByteCloud.Models;
using Login.DTO;
using Register.DTO;
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
    public string Login(Login.DTO.LoginRequest request)
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

    [HttpPost("register")]
    public string Register(Register.DTO.RegisterRequest request)
    {
        User? existingUser = context.Users.FirstOrDefault(user => user.Email == request.Email);

        if (existingUser != null)
        {
            return "Email Already Registered";
        }

        User? existingUsername = context.Users.FirstOrDefault(user => user.Username == request.Username);
        
        if (existingUsername != null)
        {
            return "Username Already Taken";
        }
        
        User user = new User
        {
            Email = request.Email,
            Username = request.Username,
            PasswordHash = passwordServices.HashPassword(new User(), request.Password),
            CreatedAt = DateTime.UtcNow,
            StorageLimit = 10737418240,
            StorageUsed = 0
        };

        context.Users.Add(user);
        context.SaveChanges();

        return "User Registered Successfully";
    }
}