using ChatService.Application.Entities;
using ChatService.Application.Interfaces;
using ChatService.Application.Persistence;

namespace ChatService.Application.Repositories;

public class ChatRepository(ChatDbContext context) : IChatRepository
{
    public async Task CreateChatAsync(ChatEntity chat)
    {
        await context.Chats.AddAsync(chat);
        await context.SaveChangesAsync();
    }
    
    public async Task AddChatMemberAsync(ChatMemberEntity chatMember)
    {
        await context.ChatMembers.AddAsync(chatMember);
        await context.SaveChangesAsync();
    }
}