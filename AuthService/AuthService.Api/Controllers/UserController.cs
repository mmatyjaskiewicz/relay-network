using AuthService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService) : ControllerBase
{
    [HttpGet("{username}/exists")]
    public async Task<IActionResult> UserExists(string username)
    {
        var exists = await userService.UserExistsAsync(username);
        if (!exists)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetUser(string username)
    {
        var userData = await userService.GetUserByUsernameAsync(username);
        
        return Ok(userData);
    }
}