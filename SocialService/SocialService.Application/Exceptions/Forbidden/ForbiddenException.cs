namespace SocialService.Application.Exceptions.Forbidden;

public class ForbiddenException(string message) : AppException(message) { }