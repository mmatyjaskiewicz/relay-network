using System.ComponentModel.DataAnnotations;

namespace SocialService.Application.Entities;

public class ProfileEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Bio { get; set; }
    
    [MaxLength(255)]
    public string? AvatarFileName { get; set; }
}