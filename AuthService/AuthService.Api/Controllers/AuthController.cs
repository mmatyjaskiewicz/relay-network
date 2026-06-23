using AuthService.Application.DTOs.Requests;
using AuthService.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(Application.Services.AuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await authService.RegisterAsync(request);
        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await authService.LoginAsync(request);
        if (!result.Success)
        {
            throw new InvalidCredentialsException();
        }
        
        Response.Cookies.Append("jwt", result.Token!, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddHours(1)
        });
        
        return Ok(result.Token);
    }
}