using AuthService.Application.Entities;

namespace AuthService.Application.Interfaces;

public interface IUserRepository
{
    public Task<UserEntity?> GetUserByUsernameAsync(string username);
    public Task CreateUserAsync(UserEntity user);
}