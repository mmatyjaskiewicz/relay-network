namespace AuthService.Application.DTOs.Requests;

public record LoginRequest(string Username, string Password);
public record RegisterRequest(string Username, string Password);