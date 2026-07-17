using SocialService.Application.Entities;

namespace SocialService.Application.Interfaces;

public interface IProfileRepository
{
    public Task CreateProfileAsync(ProfileEntity profile);
    public Task UpdateBioAsync(ProfileEntity profile, string bio);
    public Task<ProfileEntity> GetProfileByUserIdAsync(Guid userId);
}