using AuthService.Application.Entities;
using AuthService.Application.Interfaces;
using AuthService.Application.Persistence;

namespace AuthService.Application.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _context;
    
    public UserRepository(AuthDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateUserAsync(UserEntity user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }
}