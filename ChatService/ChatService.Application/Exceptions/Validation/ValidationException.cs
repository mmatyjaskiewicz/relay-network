namespace ChatService.Application.Exceptions.Validation;

public class ValidationException(string message) : AppException(message) { }