namespace SocialService.Application.Entities;

public class FriendRequestEntity
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public DateTime CreatedAt { get; set; }
}