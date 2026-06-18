using AuthService.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Application.Persistence;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users => Set<UserEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}