using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SocialService.Application.Clients;
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
        
        var result = await friendshipService.SendFriendRequestAsync(Guid.Parse(senderId), request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("accept-request/{requestId}")]
    public async Task<IActionResult> AcceptFriendRequest([FromRoute] Guid requestId)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null)
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }
        
        var result = await friendshipService.AcceptFriendRequestAsync(Guid.Parse(userId), requestId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("decline-request/{requestId}")]
    public async Task<IActionResult> DeclineFriendRequest([FromRoute] Guid requestId)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null)
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }
        
        var result = await friendshipService.DeclineFriendRequestAsync(Guid.Parse(userId), requestId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("remove-friend")]
    public async Task<IActionResult> RemoveFriendship([FromBody] RemoveFriendshipDto request)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null)
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }
        
        var result = await friendshipService.RemoveFriendshipAsync(Guid.Parse(userId), request);
        
        return result.Success ? Ok(result) : BadRequest(result);
    }
}