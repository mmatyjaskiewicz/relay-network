namespace AuthService.Application.Exceptions.Conflict;

public class UserAlreadyExistsException(string username) : ConflictException($"User with username '{username}' already exists.") { }