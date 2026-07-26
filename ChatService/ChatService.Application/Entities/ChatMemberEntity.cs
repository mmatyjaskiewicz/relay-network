namespace ChatService.Application.Entities;

public class ChatMemberEntity
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }

    public ChatEntity? Chat { get; set; } = null;
}