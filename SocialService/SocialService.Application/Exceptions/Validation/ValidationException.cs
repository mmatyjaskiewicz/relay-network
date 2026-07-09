namespace SocialService.Application.Exceptions.Validation;

public class ValidationException(string message) : AppException(message) { }