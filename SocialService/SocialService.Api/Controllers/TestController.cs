using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocialService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [Authorize]
    [HttpGet("me")]
    public IActionResult Test()
    {
        var userId = User.FindFirst("userId")?.Value;
        var username = User.FindFirst("username")?.Value;

        return Ok(new
        {
            userId,
            username
        });
    }
}