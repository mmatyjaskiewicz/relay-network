using SocialService.Application.Clients;
using SocialService.Application.DTOs.Results;
using SocialService.Application.Interfaces;

namespace SocialService.Application.Services;

public class FriendshipService(IFriendshipRepository friendshipRepository, AuthClient authClient)
{
    public async Task<FriendshipResult> SendFriendRequestAsync(Guid senderId, string receiverUsername)
    {
        if(senderId == Guid.Empty)
        {
            return new FriendshipResult { Success = false, Message = "Invalid sender ID." };
        }
        
        if(string.IsNullOrWhiteSpace(receiverUsername))
        {
            return new FriendshipResult { Success = false, Message = "Receiver username cannot be empty." };
        }
        
        var receiverExists = await authClient.UserExistsAsync(receiverUsername);
        if (!receiverExists)
        {
            return new FriendshipResult { Success = false, Message = "Receiver user does not exist." };
        }
        
        var receiver = await authClient.GetUserDataAsync(receiverUsername);
        var receiverId = receiver.Id;
        
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
    
    public async Task<FriendshipResult> AcceptFriendRequestAsync(Guid requestId)
    {
        if(requestId == Guid.Empty)
        {
            return new FriendshipResult { Success = false, Message = "Invalid request ID." };
        }
        
        var requestExists = await friendshipRepository.FriendRequestExistsByIdAsync(requestId);
        if (!requestExists)
        {
            return new FriendshipResult { Success = false, Message = "Friend request does not exist." };
        }
        
        var success = await friendshipRepository.AcceptFriendRequestAsync(requestId);
        if (!success)
        {
            return new FriendshipResult { Success = false, Message = "Failed to accept friend request." };
        }
        
        return new FriendshipResult { Success = true, Message = "Friend request accepted successfully." };
    }
    
    public async Task<FriendshipResult> DeclineFriendRequestAsync(Guid requestId)
    {
        if(requestId == Guid.Empty)
        {
            return new FriendshipResult { Success = false, Message = "Invalid request ID." };
        }
        
        var requestExists = await friendshipRepository.FriendRequestExistsByIdAsync(requestId);
        if (!requestExists)
        {
            return new FriendshipResult { Success = false, Message = "Friend request does not exist." };
        }
        
        var success = await friendshipRepository.DeclineFriendRequestAsync(requestId);
        if (!success)
        {
            return new FriendshipResult { Success = false, Message = "Failed to decline friend request." };
        }
        
        return new FriendshipResult { Success = true, Message = "Friend request declined successfully." };
    }
}