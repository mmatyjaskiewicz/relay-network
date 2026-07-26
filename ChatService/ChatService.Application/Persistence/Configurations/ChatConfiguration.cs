using ChatService.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Application.Persistence.Configurations;

public class ChatConfiguration : IEntityTypeConfiguration<ChatEntity>
{
    public void Configure(EntityTypeBuilder<ChatEntity> builder)
    {
        builder.ToTable("chats");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Type).IsRequired();
        
        builder.Property(c => c.Name).HasMaxLength(100);
    }
}