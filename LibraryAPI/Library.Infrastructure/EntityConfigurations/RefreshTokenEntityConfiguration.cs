using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.EntityConfigurations;

public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.Token)
            .IsRequired();
        builder.Property(rt => rt.ExpiryDate)
            .IsRequired();
        builder.Property(rt => rt.AddedDate)
            .IsRequired();
        builder.Property(rt => rt.IsUsed)
            .IsRequired();
        builder.Property(rt => rt.IsRevoked)
            .IsRequired();
        builder.Property(rt => rt.ReplacedByToken)
            .IsUnicode(false)
            .HasMaxLength(200);

        builder.HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(rt => rt.Token).IsUnique();
        builder.HasIndex(rt => rt.UserId);
    }
}