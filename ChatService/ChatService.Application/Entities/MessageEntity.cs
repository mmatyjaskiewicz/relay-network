namespace ChatService.Application.Entities;

public class MessageEntity
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; } = string.Empty;

    public ChatEntity? Chat { get; set; } = null;
}