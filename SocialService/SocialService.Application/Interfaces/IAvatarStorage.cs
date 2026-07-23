using SocialService.Application.DTOs.Requests;

namespace SocialService.Application.Interfaces;

public interface IAvatarStorage
{
    Task<string> UploadAsync(UploadAvatarRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(string objectName, CancellationToken cancellationToken = default);
}