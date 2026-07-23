using Microsoft.AspNetCore.Mvc;
using SocialService.Application.DTOs.Requests;
using SocialService.Application.Interfaces;
using SocialService.Application.Services;

namespace SocialService.Api.Controllers;

[ApiController]
[Route("api/profile")]
public class ProfileController(ProfileService profileService) : ControllerBase
{
    [HttpPost("avatar")]
    public async Task<IActionResult> UploadAvatar(IFormFile file, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null)
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }

        await profileService.UpdateAvatarAsync(new UploadAvatarRequest(file.OpenReadStream(), file.FileName, file.ContentType, file.Length), cancellationToken, Guid.Parse(userId));
        return NoContent();
    }

    [HttpDelete("avatar")]
    public async Task<IActionResult> DeleteAvatar(CancellationToken cancellationToken)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null)
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }

        await profileService.DeleteAvatarAsync(Guid.Parse(userId), cancellationToken);
        return NoContent();
    }

    [HttpPut("bio")]
    public async Task<IActionResult> UpdateBio([FromBody] string bio)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (userId == null)
        {
            return Unauthorized(new { Message = "Invalid or missing user ID." });
        }
        
        await profileService.UpdateBioAsync(Guid.Parse(userId), bio);
        return NoContent();
    }
}