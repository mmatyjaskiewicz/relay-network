using SocialService.Application.Entities;

namespace SocialService.Application.Interfaces;

public interface IProfileRepository
{
    public Task CreateProfileAsync(ProfileEntity profile);
    public Task UpdateProfileAsync(ProfileEntity profile);
    public Task<ProfileEntity?> GetProfileByUserIdAsync(Guid userId);
}