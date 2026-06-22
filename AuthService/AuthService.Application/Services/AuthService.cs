using AuthService.Application.DTOs.Requests;
using AuthService.Application.DTOs.Results;
using AuthService.Application.Entities;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using FluentValidation;

namespace AuthService.Application.Services;

public class AuthService(IUserRepository userRepository, IValidator<RegisterRequest> registerValidator)
{
    public async Task<RegisterResult> RegisterAsync(RegisterRequest request)
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
        
        return new RegisterResult
        {
            Success = true,
            Message = "User registered successfully."
        };
    }
    
    public async Task<LoginResult> LoginAsync(LoginRequest request)
    {
        var user = await userRepository.GetUserByUsernameAsync(request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }
        
        string token = "MockAccessToken";
        
        return new LoginResult
        {
            Success = true,
            Message = "Login successful.",
            Token = token
        };
    }
}