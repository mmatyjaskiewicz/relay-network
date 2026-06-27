namespace AuthService.Application.Exceptions.NotFound;

public abstract class NotFoundException(string message) : AppException(message) { }