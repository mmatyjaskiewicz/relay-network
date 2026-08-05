using ChatService.Application.DTOs;
using ChatService.Application.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Api.Hubs;

public class ChatHub(ChatManager chatManager) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }
        await base.OnConnectedAsync();
    }
    
    public async Task SendMessage(SendMessageRequest request)
    {
        var userId = Guid.Parse(Context.UserIdentifier!);

        var message = await chatManager.SendMessageAsync(userId, request);

        var members = await chatManager.GetChatMembersAsync(request.ChatId);

        foreach (var member in members)
        {
            await Clients.User(member.ToString()).SendAsync("ReceiveMessage", message);
        }
    }
}