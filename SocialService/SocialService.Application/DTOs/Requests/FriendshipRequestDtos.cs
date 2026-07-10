namespace SocialService.Application.DTOs.Requests;

public record SendFriendRequestDto(string Username);

public record RemoveFriendshipDto(string Username);

public record AcceptFriendRequestDto(Guid RequestId);

public record DeclineFriendRequestDto(Guid RequestId);