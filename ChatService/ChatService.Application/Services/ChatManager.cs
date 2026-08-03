using ChatService.Application.DTOs;
using ChatService.Application.Entities;
using ChatService.Application.Enums;
using ChatService.Application.Interfaces;

namespace ChatService.Application.Services;

public class ChatManager(IChatRepository chatRepository)
{
    public async Task CreatePrivateChatAsync(Guid userId, Guid friendId)
    {
        var chat = new ChatEntity
        {
            Id = Guid.NewGuid(),
            Type = ChatType.Direct
        };

        await chatRepository.CreateChatAsync(chat);

        await chatRepository.AddChatMemberAsync(new ChatMemberEntity
        {
            Id = Guid.NewGuid(),
            ChatId = chat.Id,
            UserId = userId
        });

        await chatRepository.AddChatMemberAsync(new ChatMemberEntity
        {
            Id = Guid.NewGuid(),
            ChatId = chat.Id,
            UserId = friendId
        });
    }
    
    public async Task<List<ChatEntity>> GetChatsAsync(Guid userId)
    {
        return await chatRepository.GetChatsAsync(userId);
    }
    
    public async Task<List<MessageEntity>> GetMessagesAsync(Guid chatId)
    {
        return await chatRepository.GetMessagesAsync(chatId);
    }
    
    public async Task SendMessageAsync(Guid senderId, SendMessageRequest request)
    {
        var message = new MessageEntity
        {
            Id = Guid.NewGuid(),
            ChatId = request.ChatId,
            SenderId = senderId,
            Content = request.Content,
            SentAt = DateTime.UtcNow
        };

        await chatRepository.SendMessageAsync(message);
    }
}