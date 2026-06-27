namespace AuthService.Application.Exceptions.Unauthorized;

public class InvalidCredentialsException() : UnauthorizedException("Invalid credentials.") { }