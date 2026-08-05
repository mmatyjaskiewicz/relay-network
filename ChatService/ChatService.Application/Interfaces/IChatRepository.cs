using ChatService.Application.Entities;

namespace ChatService.Application.Interfaces;

public interface IChatRepository
{
    public Task CreateChatAsync(ChatEntity chat);
    public Task AddChatMemberAsync(ChatMemberEntity chatMember);
    public Task<List<ChatEntity>> GetChatsAsync(Guid userId);
    public Task<List<MessageEntity>> GetMessagesAsync(Guid chatId);
    public Task<MessageEntity> SendMessageAsync(MessageEntity message);
    Task<List<Guid>> GetChatMembersAsync(Guid chatId);
}