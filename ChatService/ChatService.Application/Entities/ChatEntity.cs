using ChatService.Application.Enums;

namespace ChatService.Application.Entities;

public class ChatEntity
{
    public Guid Id { get; set; }
    public ChatType Type { get; set; }
    public string? Name { get; set; }
}