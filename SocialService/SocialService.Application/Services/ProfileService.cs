using SocialService.Application.DTOs.Requests;
using SocialService.Application.Entities;
using SocialService.Application.Exceptions.NotFound;
using SocialService.Application.Interfaces;

namespace SocialService.Application.Services;

public class ProfileService(IProfileRepository profileRepository, IAvatarStorage avatarStorage)
{
    public async Task CreateProfileAsync(Guid userId, string username)
    {
        var profile = new ProfileEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Username = username,
        };

        await profileRepository.CreateProfileAsync(profile);
    }
    
    public async Task UpdateBioAsync(Guid userId, string bio)
    {
        var profile = await profileRepository.GetProfileByUserIdAsync(userId);
        if (profile == null)
        {
            throw new NotFoundException("Profile not found.");
        }
        
        await profileRepository.UpdateBioAsync(profile, bio);
    }
    
    public async Task UpdateAvatarAsync(UploadAvatarRequest request, CancellationToken cancellationToken, Guid userId)
    {
        var profile = await profileRepository.GetProfileByUserIdAsync(userId);
        if (profile == null)
        {
            throw new NotFoundException("Profile not found.");
        }
        
        var objectName = await avatarStorage.UploadAsync(request, cancellationToken);
        profile.AvatarFileName = objectName;
        await profileRepository.UpdateAvatarAsync(profile, objectName);
    }
}