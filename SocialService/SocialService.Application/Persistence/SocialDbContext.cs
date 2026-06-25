using Microsoft.EntityFrameworkCore;
using SocialService.Application.Entities;

namespace SocialService.Application.Persistence;

public class SocialDbContext(DbContextOptions<SocialDbContext> options) : DbContext(options)
{
    public DbSet<FriendRequestEntity> FriendRequests => Set<FriendRequestEntity>();
    public DbSet<FriendshipEntity> Friendships => Set<FriendshipEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SocialDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
    
}