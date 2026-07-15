using SocialService.Application.Entities;
using SocialService.Application.Interfaces;

namespace SocialService.Application.Services;

public class ProfileService(IProfileRepository profileRepository)
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
}