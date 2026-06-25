using Microsoft.EntityFrameworkCore;
using SocialService.Application.Entities;

namespace SocialService.Application.Persistence.Configurations;

public class FriendRequestConfiguration : IEntityTypeConfiguration<FriendRequestEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FriendRequestEntity> builder)
    {
        builder.ToTable("friend_requests");
        
        builder.HasKey(fr => fr.Id);
        builder.Property(fr => fr.Id).HasColumnName("id");
        
        builder.Property(fr => fr.SenderId).HasColumnName("sender_id").IsRequired();
        builder.Property(fr => fr.ReceiverId).HasColumnName("receiver_id").IsRequired();
        builder.Property(fr => fr.CreatedAt).HasColumnName("created_at").IsRequired();
    }
}