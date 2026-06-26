using SocialService.Application.DTOs.Results;
using SocialService.Application.Interfaces;

namespace SocialService.Application.Services;

public class FriendshipService(IFriendshipRepository friendshipRepository)
{
    public async Task<FriendshipResult> SendFriendRequestAsync(Guid senderId, Guid receiverId)
    {
        if(senderId == Guid.Empty || receiverId == Guid.Empty)
        {
            return new FriendshipResult { Success = false, Message = "Invalid user IDs." };
        }
        
        // Implement logic to check if receiver exists
        // PLACEHOLDER
        
        if (senderId == receiverId)
        {
            return new FriendshipResult { Success = false, Message = "Cannot send friend request to yourself." };
        }
        
        var existingFriendship = await friendshipRepository.FriendshipExistsAsync(senderId, receiverId);
        if (existingFriendship)
        {
            return new FriendshipResult { Success = false, Message = "Friendship already exists." };
        }
        
        var existingRequest = await friendshipRepository.FriendRequestExistsAsync(senderId, receiverId);
        if (existingRequest)
        {
            return new FriendshipResult { Success = false, Message = "Friend request already sent." };
        }

        await friendshipRepository.SendFriendRequestAsync(senderId, receiverId);
        return new FriendshipResult { Success = true, Message = "Friend request sent successfully." };
    }
}