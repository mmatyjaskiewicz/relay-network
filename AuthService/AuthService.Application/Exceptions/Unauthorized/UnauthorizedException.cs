namespace AuthService.Application.Exceptions.Unauthorized;

public class UnauthorizedException(string message) : AppException(message) { }