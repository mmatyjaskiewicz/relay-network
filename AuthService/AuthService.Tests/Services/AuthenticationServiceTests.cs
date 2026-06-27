using AuthService.Application.DTOs.Requests;
using AuthService.Application.Entities;
using AuthService.Application.Interfaces;
using AuthService.Application.Security;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using Moq;

namespace AuthService.Tests.Services;

public class AuthenticationServiceTests
{
    [Fact]
    public async Task RegisterAsync_ShouldCreateUser_WhenRequestIsValid()
    {
        // Arrange
        var repositoryMock = new Mock<IUserRepository>();
        var registerValidatorMock = new Mock<IValidator<RegisterRequest>>();
        var loginValidatorMock = new Mock<IValidator<LoginRequest>>();
        
        registerValidatorMock
            .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<RegisterRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        var jwtSettings = Options.Create(new JwtSettings
        {
            Key = "super-secret-key-super-secret-key",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            ExpirationInMinutes = 60
        });

        var jwtGenerator = new JwtGenerator(jwtSettings);
        
        var request = new RegisterRequest
        {
            Username = "testuser",
            Password = "password"
        };
        
        repositoryMock.Setup(r => r.GetUserByUsernameAsync(request.Username)).ReturnsAsync((UserEntity?)null);
        
        var authService = new Application.Services.AuthenticationService(repositoryMock.Object, jwtGenerator, registerValidatorMock.Object, loginValidatorMock.Object);
        
        await authService.RegisterAsync(request);
        
        // Assert
        repositoryMock.Verify(r => r.CreateUserAsync(It.IsAny<UserEntity>()), Times.Once);
    }
    
    [Fact]
    public async Task RegisterAsync_ShouldReturnFailure_WhenUsernameAlreadyExists()
    {
        // Arrange
        var repositoryMock = new Mock<IUserRepository>();
        var registerValidatorMock = new Mock<IValidator<RegisterRequest>>();
        var loginValidatorMock = new Mock<IValidator<LoginRequest>>();

        registerValidatorMock
            .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<RegisterRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var jwtSettings = Options.Create(new JwtSettings
        {
            Key = "super-secret-key-super-secret-key",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            ExpirationInMinutes = 60
        });

        var jwtGenerator = new JwtGenerator(jwtSettings);

        var authService = new Application.Services.AuthenticationService(repositoryMock.Object, jwtGenerator, registerValidatorMock.Object, loginValidatorMock.Object);

        var request = new RegisterRequest
        {
            Username = "testuser",
            Password = "password123"
        };

        repositoryMock
            .Setup(r => r.GetUserByUsernameAsync(request.Username))
            .ReturnsAsync(new UserEntity());

        // Act
        var result = await authService.RegisterAsync(request);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Be("Username already exists.");

        repositoryMock.Verify(r => r.CreateUserAsync(It.IsAny<UserEntity>()), Times.Never);
    }
    
    [Fact]
    public async Task LoginAsync_ShouldReturnSuccess_WhenCredentialsAreValid()
    {
        // Arrange
        var repositoryMock = new Mock<IUserRepository>();
        var registerValidatorMock = new Mock<IValidator<RegisterRequest>>();
        var loginValidatorMock = new Mock<IValidator<LoginRequest>>();

        loginValidatorMock
            .Setup(v => v.ValidateAsync(
                It.IsAny<ValidationContext<LoginRequest>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var jwtSettings = Options.Create(new JwtSettings
        {
            Key = "super-secret-key-super-secret-key",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            ExpirationInMinutes = 60
        });

        var jwtGenerator = new JwtGenerator(jwtSettings);

        var authService = new Application.Services.AuthenticationService(
            repositoryMock.Object,
            jwtGenerator,
            registerValidatorMock.Object,
            loginValidatorMock.Object);

        var request = new LoginRequest
        {
            Username = "testuser",
            Password = "password123"
        };

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123")
        };

        repositoryMock
            .Setup(r => r.GetUserByUsernameAsync(request.Username))
            .ReturnsAsync(user);

        // Act
        var result = await authService.LoginAsync(request);

        // Assert
        result.Success.Should().BeTrue();
        result.Message.Should().Be("Login successful.");
        result.Token.Should().NotBeNullOrWhiteSpace();
    }
    
    [Fact]
    public async Task LoginAsync_ShouldReturnFailure_WhenUserDoesNotExist()
    {
        // Arrange
        var repositoryMock = new Mock<IUserRepository>();
        var registerValidatorMock = new Mock<IValidator<RegisterRequest>>();
        var loginValidatorMock = new Mock<IValidator<LoginRequest>>();

        loginValidatorMock
            .Setup(v => v.ValidateAsync(
                It.IsAny<ValidationContext<LoginRequest>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var jwtSettings = Options.Create(new JwtSettings
        {
            Key = "super-secret-key-super-secret-key",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            ExpirationInMinutes = 60
        });

        var jwtGenerator = new JwtGenerator(jwtSettings);

        var authService = new Application.Services.AuthenticationService(
            repositoryMock.Object,
            jwtGenerator,
            registerValidatorMock.Object,
            loginValidatorMock.Object);

        var request = new LoginRequest
        {
            Username = "testuser",
            Password = "password123"
        };

        repositoryMock
            .Setup(r => r.GetUserByUsernameAsync(request.Username))
            .ReturnsAsync((UserEntity?)null);

        // Act
        var result = await authService.LoginAsync(request);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Be("Invalid username or password.");
    }
    
    [Fact]
    public async Task LoginAsync_ShouldReturnFailure_WhenPasswordIsInvalid()
    {
        // Arrange
        var repositoryMock = new Mock<IUserRepository>();
        var registerValidatorMock = new Mock<IValidator<RegisterRequest>>();
        var loginValidatorMock = new Mock<IValidator<LoginRequest>>();

        loginValidatorMock
            .Setup(v => v.ValidateAsync(
                It.IsAny<ValidationContext<LoginRequest>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var jwtSettings = Options.Create(new JwtSettings
        {
            Key = "super-secret-key-super-secret-key",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            ExpirationInMinutes = 60
        });

        var jwtGenerator = new JwtGenerator(jwtSettings);

        var authService = new Application.Services.AuthenticationService(
            repositoryMock.Object,
            jwtGenerator,
            registerValidatorMock.Object,
            loginValidatorMock.Object);

        var request = new LoginRequest
        {
            Username = "testuser",
            Password = "wrongpassword"
        };

        var user = new UserEntity
        {
            Username = "testuser",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("correctpassword")
        };

        repositoryMock
            .Setup(r => r.GetUserByUsernameAsync(request.Username))
            .ReturnsAsync(user);

        // Act
        var result = await authService.LoginAsync(request);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Be("Invalid username or password.");
    }
}