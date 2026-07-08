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
        
        var friendship1 = new FriendshipEntity
        {
            Id = Guid.NewGuid(),
            UserId = friendRequest!.SenderId,
            FriendId = friendRequest.ReceiverId,
            CreatedAt = DateTime.UtcNow
        };
        
        var friendship2 = new FriendshipEntity
        {
            Id = Guid.NewGuid(),
            UserId = friendRequest.ReceiverId,
            FriendId = friendRequest.SenderId,
            CreatedAt = DateTime.UtcNow
        };

        await dbContext.Friendships.AddAsync(friendship1);
        await dbContext.Friendships.AddAsync(friendship2);
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
    
    public async Task<bool> RemoveFriendshipAsync(Guid userId, Guid friendId)
    {
        var friendship1 = await dbContext.Friendships.FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);
        var friendship2 = await dbContext.Friendships.FirstOrDefaultAsync(f => f.UserId == friendId && f.FriendId == userId);

        if (friendship1 != null)
        {
            dbContext.Friendships.Remove(friendship1);
        }

        if (friendship2 != null)
        {
            dbContext.Friendships.Remove(friendship2);
        }

        await dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> FriendshipExistsAsync(Guid userId, Guid friendId)
    {
        return await dbContext.Friendships.AnyAsync(f => (f.UserId == userId && f.FriendId == friendId) || (f.UserId == friendId && f.FriendId == userId));
    }
    
    public async Task<bool> FriendRequestExistsAsync(Guid senderId, Guid receiverId)
    {
        return await dbContext.FriendRequests.AnyAsync(fr => fr.SenderId == senderId && fr.ReceiverId == receiverId || fr.SenderId == receiverId && fr.ReceiverId == senderId);
    }
    
    public async Task<bool> FriendRequestExistsByIdAsync(Guid requestId)
    {
        return await dbContext.FriendRequests.AnyAsync(fr => fr.Id == requestId);
    }
    
    public async Task<FriendRequestEntity?> GetFriendRequestByIdAsync(Guid requestId)
    {
        return await dbContext.FriendRequests.FindAsync(requestId);
    }
}