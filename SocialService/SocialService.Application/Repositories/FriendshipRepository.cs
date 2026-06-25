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
}