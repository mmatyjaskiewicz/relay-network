using Microsoft.EntityFrameworkCore;
using SocialService.Application.Entities;

namespace SocialService.Application.Persistence.Configurations;

public class FriendshipConfiguration : IEntityTypeConfiguration<FriendshipEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<FriendshipEntity> builder)
    {
        builder.ToTable("friendships");
        
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).HasColumnName("id");
        
        builder.Property(f => f.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(f => f.FriendId).HasColumnName("friend_id").IsRequired();
        builder.Property(f => f.CreatedAt).HasColumnName("created_at").IsRequired();
    }
}