using AuthService.Application.DTOs.Requests;
using AuthService.Application.Entities;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using FluentValidation;

namespace AuthService.Application.Services;

public class AuthService(IUserRepository userRepository, IValidator<RegisterRequest> registerValidator)
{
    public async Task RegisterAsync(RegisterRequest request)
    {
        await registerValidator.ValidateAndThrowAsync(request);
        
        var existingUser = await userRepository.GetUserByUsernameAsync(request.Username);
        if (existingUser != null)
        {
            throw new UserAlreadyExistsException(request.Username);
        }
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = passwordHash
        };

        await userRepository.CreateUserAsync(user);
    }
}