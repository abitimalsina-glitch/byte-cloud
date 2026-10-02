using ByteCloud.DTO;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/login")]
public class AuthController : ControllerBase
{
    [HttpPost]
    public string Login(LoginRequest request)
    {
        return $"{request.Email} | {request.Password}";
    }
}