using AuthService.Application.Entities;
using AuthService.Application.Interfaces;
using AuthService.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Application.Repositories;

public class UserRepository(AuthDbContext context) : IUserRepository
{ 
    public async Task<UserEntity?> GetUserByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
    
    public async Task CreateUserAsync(UserEntity user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }
}