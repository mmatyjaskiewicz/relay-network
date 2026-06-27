using AuthService.Application.DTOs.Requests;
using AuthService.Application.DTOs.Results;
using AuthService.Application.Entities;
using AuthService.Application.Exceptions.Conflict;
using AuthService.Application.Exceptions.Unauthorized;
using AuthService.Application.Interfaces;
using AuthService.Application.Security;
using FluentValidation;

namespace AuthService.Application.Services;

public class AuthenticationService(IUserRepository userRepository,JwtGenerator jwtGenerator,
    IValidator<RegisterRequest> registerValidator, IValidator<LoginRequest> loginValidator)
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
        await loginValidator.ValidateAndThrowAsync(request);
        
        var user = await userRepository.GetUserByUsernameAsync(request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }
        
        string token = jwtGenerator.GenerateToken(user);
        
        return new LoginResult
        {
            Success = true,
            Message = "Login successful.",
            Token = token
        };
    }
}