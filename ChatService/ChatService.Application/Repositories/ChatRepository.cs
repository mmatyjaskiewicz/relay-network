using ChatService.Application.Entities;
using ChatService.Application.Interfaces;
using ChatService.Application.Persistence;
using Microsoft.EntityFrameworkCore;

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
    
    public async Task<List<ChatEntity>> GetChatsAsync(Guid userId)
    {
        return await context.ChatMembers
            .Where(cm => cm.UserId == userId)
            .Select(cm => cm.Chat!)
            .ToListAsync();
    }

    public async Task<List<MessageEntity>> GetMessagesAsync(Guid chatId)
    {
        return await context.Messages
            .Where(m => m.ChatId == chatId)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }

    public async Task<MessageEntity> SendMessageAsync(MessageEntity message)
    {
        await context.Messages.AddAsync(message);
        await context.SaveChangesAsync();

        return message;
    }
    
    public async Task<List<Guid>> GetChatMembersAsync(Guid chatId)
    {
        return await context.ChatMembers
            .Where(cm => cm.ChatId == chatId)
            .Select(cm => cm.UserId)
            .ToListAsync();
    }
}