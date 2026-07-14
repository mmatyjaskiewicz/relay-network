using AuthService.Application.DTOs.Requests;
using AuthService.Application.Entities;
using AuthService.Application.Exceptions.Conflict;
using AuthService.Application.Exceptions.Unauthorized;
using AuthService.Application.Interfaces;
using AuthService.Application.Security;
using FluentValidation;
using MassTransit;
using Shared.Contracts.Events;

namespace AuthService.Application.Services;

public class AuthenticationService(IUserRepository userRepository,JwtGenerator jwtGenerator, IPublishEndpoint publishEndpoint,
    IValidator<RegisterRequest> registerValidator, IValidator<LoginRequest> loginValidator)
{
    public async Task RegisterAsync(RegisterRequest request)
    {
        await registerValidator.ValidateAndThrowAsync(request);
        
        var existingUser = await userRepository.GetUserByUsernameAsync(request.Username);
        if (existingUser != null)
        {
            throw new ConflictException("Username is already taken.");
        }
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = passwordHash
        };
        
        await userRepository.CreateUserAsync(user);
        
        await publishEndpoint.Publish(new UserRegistered(user.Id, user.Username));
    }
    
    public async Task<string> LoginAsync(LoginRequest request)
    {
        await loginValidator.ValidateAndThrowAsync(request);
        
        var user = await userRepository.GetUserByUsernameAsync(request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Invalid username or password.");
        }
        
        var token = jwtGenerator.GenerateToken(user);
        
        return token;
    }
}