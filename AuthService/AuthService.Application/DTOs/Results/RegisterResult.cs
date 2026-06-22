namespace AuthService.Application.DTOs.Results;

public class RegisterResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}