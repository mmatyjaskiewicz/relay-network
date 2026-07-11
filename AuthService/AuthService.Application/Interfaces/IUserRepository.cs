using AuthService.Application.Entities;

namespace AuthService.Application.Interfaces;

public interface IUserRepository
{
    public Task CreateUserAsync(UserEntity user);
    public Task<UserEntity?> GetUserByUsernameAsync(string username);
    public Task<bool> UserExistsAsync(string username);
}