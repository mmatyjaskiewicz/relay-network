using System.ComponentModel.DataAnnotations;

namespace ChatService.Application.Entities;

public class MessageEntity
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Guid SenderId { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
        
    [MaxLength(1000)]
    public string Content { get; set; } = string.Empty;

    public ChatEntity? Chat { get; set; } = null;
}