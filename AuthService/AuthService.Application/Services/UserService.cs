using AuthService.Application.DTOs.Responses;
using AuthService.Application.Exceptions.BadRequest;
using AuthService.Application.Exceptions.NotFound;
using AuthService.Application.Interfaces;

namespace AuthService.Application.Services;

public class UserService(IUserRepository userRepository)
{
    public async Task<bool> UserExistsAsync(string username)
    {
        if(username == null)
        {
            throw new BadRequestException("Username cannot be null.");
        }
        
        return await userRepository.UserExistsAsync(username);
    }
    
    public async Task<GetUserResponse> GetUserByUsernameAsync(string username)
    {
        var user = await userRepository.GetUserByUsernameAsync(username);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }
        
        return new GetUserResponse
        {
            Id = user.Id,
            Username = user.Username
        };
    }
}