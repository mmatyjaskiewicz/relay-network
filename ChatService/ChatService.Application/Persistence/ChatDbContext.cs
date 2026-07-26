using ChatService.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Application.Persistence;

public class ChatDbContext(DbContextOptions<ChatDbContext> options) : DbContext(options)
{
    public DbSet<ChatEntity> Chats => Set<ChatEntity>();
    public DbSet<ChatMemberEntity> ChatMembers => Set<ChatMemberEntity>();
    public DbSet<MessageEntity> Messages => Set<MessageEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChatDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}