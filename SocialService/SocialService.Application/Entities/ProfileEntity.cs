namespace SocialService.Application.Entities;

public class ProfileEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? AvatarFileName { get; set; }
}