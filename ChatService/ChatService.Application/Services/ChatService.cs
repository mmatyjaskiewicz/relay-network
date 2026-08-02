using ChatService.Application.Entities;
using ChatService.Application.Enums;
using ChatService.Application.Interfaces;

namespace ChatService.Application.Services;

public class ChatService(IChatRepository chatRepository)
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
            ChatId = chat.Id,
            UserId = userId
        });

        await chatRepository.AddChatMemberAsync(new ChatMemberEntity
        {
            ChatId = chat.Id,
            UserId = friendId
        });
    }
}