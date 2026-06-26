using Microsoft.EntityFrameworkCore;
using SocialService.Application.Entities;
using SocialService.Application.Interfaces;
using SocialService.Application.Persistence;

namespace SocialService.Application.Repositories;

public class FriendshipRepository(SocialDbContext dbContext) : IFriendshipRepository
{
    public async Task<bool> SendFriendRequestAsync(Guid senderId, Guid receiverId)
    {
        var friendRequest = new FriendRequestEntity
        {
            Id = Guid.NewGuid(),
            SenderId = senderId,
            ReceiverId = receiverId,
            CreatedAt = DateTime.UtcNow
        };
        
        await dbContext.FriendRequests.AddAsync(friendRequest);
        await dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> AcceptFriendRequestAsync(Guid requestId)
    {
        var friendRequest = await dbContext.FriendRequests.FindAsync(requestId);
        if (friendRequest == null) return false;

        var friendship = new FriendshipEntity
        {
            Id = Guid.NewGuid(),
            UserId = friendRequest.SenderId,
            FriendId = friendRequest.ReceiverId,
            CreatedAt = DateTime.UtcNow
        };

        await dbContext.Friendships.AddAsync(friendship);
        dbContext.FriendRequests.Remove(friendRequest);
        await dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeclineFriendRequestAsync(Guid requestId)
    {
        var friendRequest = await dbContext.FriendRequests.FindAsync(requestId);
        if (friendRequest == null) return false;

        dbContext.FriendRequests.Remove(friendRequest);
        await dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> FriendshipExistsAsync(Guid userId, Guid friendId)
    {
        return await dbContext.Friendships.AnyAsync(f => (f.UserId == userId && f.FriendId == friendId) || (f.UserId == friendId && f.FriendId == userId));
    }
    
    public Task<bool> FriendRequestExistsAsync(Guid senderId, Guid receiverId)
    {
        return dbContext.FriendRequests.AnyAsync(fr => fr.SenderId == senderId && fr.ReceiverId == receiverId || fr.SenderId == receiverId && fr.ReceiverId == senderId);
    }
}