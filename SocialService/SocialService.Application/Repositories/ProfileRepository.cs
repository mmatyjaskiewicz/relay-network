using SocialService.Application.Entities;
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
}