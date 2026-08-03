namespace ChatService.Application.Entities;

public class MessageEntity
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public ChatEntity? Chat { get; set; } = null;
}