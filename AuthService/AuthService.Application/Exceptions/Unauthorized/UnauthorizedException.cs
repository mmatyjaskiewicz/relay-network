namespace AuthService.Application.Exceptions.Unauthorized;

public abstract class UnauthorizedException(string message) : AppException(message) { }