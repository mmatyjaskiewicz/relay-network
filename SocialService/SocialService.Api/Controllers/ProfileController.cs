using Microsoft.AspNetCore.Mvc;
using SocialService.Application.DTOs.Requests;
using SocialService.Application.Interfaces;

namespace SocialService.Api.Controllers;

[ApiController]
[Route("api/profile")]
public class ProfileController(IAvatarStorage avatarStorage) : ControllerBase
{
    [HttpPost("avatar")]
    public async Task<IActionResult> UploadAvatar(IFormFile file, CancellationToken cancellationToken)
    {
        var objectName = await avatarStorage.UploadAsync(new UploadAvatarRequest(file.OpenReadStream(), file.FileName, file.ContentType, file.Length), cancellationToken);

        return Ok(new
        {
            ObjectName = objectName
        });
    }
}