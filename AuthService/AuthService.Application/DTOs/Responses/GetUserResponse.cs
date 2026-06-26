namespace AuthService.Application.DTOs.Responses;

public class GetUserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
}