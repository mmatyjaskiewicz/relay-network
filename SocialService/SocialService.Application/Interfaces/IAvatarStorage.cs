namespace SocialService.Application.Interfaces;

public interface IAvatarStorage
{
    Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken = default);
}