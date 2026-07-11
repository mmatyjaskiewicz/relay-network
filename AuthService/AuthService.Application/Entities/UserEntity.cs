using System.ComponentModel.DataAnnotations;

namespace AuthService.Application.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;
    
    [MaxLength(255)]
    public string PasswordHash { get; set; } = string.Empty;
}