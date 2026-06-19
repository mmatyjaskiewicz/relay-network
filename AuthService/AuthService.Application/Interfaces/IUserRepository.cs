using AuthService.Application.Entities;

namespace AuthService.Application.Interfaces;

public interface IUserRepository
{
    public Task CreateUserAsync(UserEntity user);
}