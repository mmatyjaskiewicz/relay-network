using SocialService.Application.Entities;
using SocialService.Application.Exceptions.NotFound;
using SocialService.Application.Interfaces;
using SocialService.Application.Persistence;

namespace SocialService.Application.Repositories;

public class ProfileRepository(SocialDbContext context) : IProfileRepository
{
    public async Task CreateProfileAsync(ProfileEntity profile)
    {
        await context.Profiles.AddAsync(profile);
        await context.SaveChangesAsync();
    }
    
    public async Task UpdateProfileAsync(ProfileEntity profile)
    {
        context.Profiles.Update(profile);
        await context.SaveChangesAsync();
    }
    
    public async Task<ProfileEntity> GetProfileByUserIdAsync(Guid userId)
    {
        var profile = await context.Profiles.FindAsync(userId);
        
        if (profile == null)
        {
            throw new NotFoundException("Profile not found.");
        }
        
        return profile;
    }
}