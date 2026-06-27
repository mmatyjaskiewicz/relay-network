namespace AuthService.Application.Exceptions.Conflict;

public abstract class ConflictException(string message) : AppException(message) { }