using Microsoft.EntityFrameworkCore;
using SocialService.Application.Entities;

namespace SocialService.Application.Persistence.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<ProfileEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProfileEntity> builder)
    {
        builder.ToTable("profiles");
        
        builder.HasKey(fr => fr.Id);
        builder.Property(fr => fr.Id).HasColumnName("id");
        builder.Property(fr => fr.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(fr => fr.Username).HasColumnName("username").IsRequired();
    }
}