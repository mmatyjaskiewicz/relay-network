using SocialService.Application.Clients;
using SocialService.Application.DTOs.Requests;
using SocialService.Application.DTOs.Results;
using SocialService.Application.Exceptions.Conflict;
using SocialService.Application.Exceptions.Forbidden;
using SocialService.Application.Exceptions.NotFound;
using SocialService.Application.Exceptions.Validation;
using SocialService.Application.Interfaces;

namespace SocialService.Application.Services;

public class FriendshipService(IFriendshipRepository friendshipRepository, AuthClient authClient)
{
    public async Task SendFriendRequestAsync(Guid senderId, SendFriendRequestDto  request)
    {
        // TODO: Add validation through FluentValidation in the future.
        if(senderId == Guid.Empty)
        {
            throw new ValidationException("Invalid sender ID.");
        }
        
        // TODO: Add validation through FluentValidation in the future.
        if(string.IsNullOrWhiteSpace(request.Username))
        {
            throw new ValidationException("Username cannot be empty.");
        }
        
        var receiverExists = await authClient.UserExistsAsync(request.Username);
        if (!receiverExists)
        {
            throw new NotFoundException($"User '{request.Username}' does not exist.");
        }
        
        var receiver = await authClient.GetUserDataAsync(request.Username);
        var receiverId = receiver.Id;
        
        if (senderId == receiverId)
        {
            throw new ValidationException("Sender and receiver cannot be the same user.");
        }
        
        var existingFriendship = await friendshipRepository.FriendshipExistsAsync(senderId, receiverId);
        if (existingFriendship)
        {
            throw new ConflictException("Friendship already exists.");
        }
        
        var existingRequest = await friendshipRepository.FriendRequestExistsAsync(senderId, receiverId);
        if (existingRequest)
        {
            throw new ConflictException("Friend request already sent.");
        }

        await friendshipRepository.SendFriendRequestAsync(senderId, receiverId);
    }
    
    public async Task AcceptFriendRequestAsync(Guid userId, Guid requestId)
    {
        // TODO: Add validation through FluentValidation in the future.
        if(requestId == Guid.Empty)
        {
            throw new ValidationException("Invalid request ID.");
        }
        
        var request = await friendshipRepository.GetFriendRequestByIdAsync(requestId);
        if (request == null)
        {
            throw new NotFoundException("Friend request not found.");
        }

        if (request.ReceiverId != userId)
        {
            throw new ForbiddenException("You are not authorized to accept this friend request.");
        }
        
        await friendshipRepository.AcceptFriendRequestAsync(requestId);
    }
    
    public async Task DeclineFriendRequestAsync(Guid userId, Guid requestId)
    {
        // TODO: Add validation through FluentValidation in the future.
        if(requestId == Guid.Empty)
        {
            throw new ValidationException("Invalid request ID.");
        }
        
        var request = await friendshipRepository.GetFriendRequestByIdAsync(requestId);
        if (request == null)
        {
            throw new NotFoundException("Friend request not found.");
        }

        if (request.ReceiverId != userId)
        {
            throw new ForbiddenException("You are not authorized to decline this friend request.");
        }
        
        await friendshipRepository.DeclineFriendRequestAsync(requestId);
    }
    
    public async Task RemoveFriendshipAsync(Guid userId, RemoveFriendshipDto dto)
    {
        var friendEntity = await authClient.GetUserDataAsync(dto.Username);
        var friendId = friendEntity.Id;
        
        // TODO: Add validation through FluentValidation in the future.
        if(userId == Guid.Empty || friendId == Guid.Empty)
        {
            throw new ValidationException("Invalid user ID or friend ID.");
        }
        
        var existingFriendship = await friendshipRepository.FriendshipExistsAsync(userId, friendId);
        if (!existingFriendship)
        {
            throw new NotFoundException("Friendship does not exist.");
        }
        
        await friendshipRepository.RemoveFriendshipAsync(userId, friendId);
    }
}