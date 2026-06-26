using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(Application.Services.AuthService authService) : ControllerBase
{
    [HttpGet("{username}/exists")]
    public async Task<IActionResult> UserExists(string username)
    {
        var exists = await authService.UserExistsAsync(username);
        if (!exists)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetUser(string username)
    {
        var userData = await authService.GetUserByUsernameAsync(username);
        
        return Ok(userData);
    }
}