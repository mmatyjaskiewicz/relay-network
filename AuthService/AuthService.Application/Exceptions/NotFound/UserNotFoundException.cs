namespace AuthService.Application.Exceptions.NotFound;

public class UserNotFoundException() : NotFoundException("User not found.") { }