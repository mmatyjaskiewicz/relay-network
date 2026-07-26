using ChatService.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Application.Persistence.Configurations;

public class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMemberEntity>
{
    public void Configure(EntityTypeBuilder<ChatMemberEntity> builder)
    {
        builder.ToTable("chat_members");
        
        builder.HasKey(cm => cm.Id);
        
        builder.Property(cm => cm.ChatId).IsRequired();
        
        builder.HasOne(cm => cm.Chat)
            .WithMany()
            .HasForeignKey(cm => cm.ChatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}