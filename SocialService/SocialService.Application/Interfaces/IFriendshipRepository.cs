namespace SocialService.Application.Interfaces;

public interface IFriendshipRepository
{
    public Task<bool> SendFriendRequestAsync(Guid senderId, Guid receiverId);
    public Task<bool> AcceptFriendRequestAsync(Guid requestId);
    public Task<bool> DeclineFriendRequestAsync(Guid requestId);
    public Task<bool> RemoveFriendshipAsync(Guid userId, Guid friendId);
    public Task<bool> FriendshipExistsAsync(Guid userId, Guid friendId);
    public Task<bool> FriendRequestExistsAsync(Guid senderId, Guid receiverId);
    public Task<bool> FriendRequestExistsByIdAsync(Guid requestId);
}