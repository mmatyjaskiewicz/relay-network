using ChatService.Application.Entities;

namespace ChatService.Application.Interfaces;

public interface IChatRepository
{
    public Task CreateChatAsync(ChatEntity chat);
    public Task AddChatMemberAsync(ChatMemberEntity chatMember);
}