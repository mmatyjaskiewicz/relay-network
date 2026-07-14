using AuthService.Application.DTOs.Requests;
using AuthService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await authenticationService.RegisterAsync(request);
       
        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await authenticationService.LoginAsync(request);
        
        Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddHours(1)
        });
        
        return Ok(token);
    }
}