namespace SocialService.Application.Interfaces;

public interface IFriendshipRepository
{
    public Task<bool> SendFriendRequestAsync(Guid senderId, Guid receiverId);
    public Task<bool> FriendshipExistsAsync(Guid userId, Guid friendId);
    public Task<bool> FriendRequestExistsAsync(Guid senderId, Guid receiverId);
}