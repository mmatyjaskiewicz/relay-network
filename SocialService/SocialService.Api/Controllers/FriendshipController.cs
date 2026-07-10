using Microsoft.AspNetCore.Mvc;
using SocialService.Application.DTOs.Requests;
using SocialService.Application.Services;

namespace SocialService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FriendshipController(FriendshipService friendshipService) : ControllerBase
{
    [HttpPost("send-request")]
    public async Task<IActionResult> SendFriendRequest([FromBody] SendFriendRequestDto request)
    {
        var senderId = User.FindFirst("userId")?.Value;
        if(senderId == null)
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }
        
        await friendshipService.SendFriendRequestAsync(Guid.Parse(senderId), request);
        return NoContent();
    }

    [HttpPost("accept-request")]
    public async Task<IActionResult> AcceptFriendRequest([FromBody] AcceptFriendRequestDto request)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null)
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }
        
        await friendshipService.AcceptFriendRequestAsync(Guid.Parse(userId), request);
        return NoContent();
    }

    [HttpPost("decline-request")]
    public async Task<IActionResult> DeclineFriendRequest([FromBody] DeclineFriendRequestDto request)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null)
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }
        
        await friendshipService.DeclineFriendRequestAsync(Guid.Parse(userId), request);
        return NoContent();
    }

    [HttpDelete("remove-friend")]
    public async Task<IActionResult> RemoveFriendship([FromBody] RemoveFriendshipDto request)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null)
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }
        
        await friendshipService.RemoveFriendshipAsync(Guid.Parse(userId), request);
        return NoContent();
    }
}