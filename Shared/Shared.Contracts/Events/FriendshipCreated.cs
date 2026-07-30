namespace Shared.Contracts.Events;

public record FriendshipCreated(Guid UserId, Guid FriendId);