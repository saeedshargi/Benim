using Benim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benim.Infrastructure.Data.Configs;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.BirthDate).IsRequired(false);
        builder.Property(x => x.RefreshToken).IsRequired(false).HasMaxLength(512);
        builder.Property(x => x.RefreshTokenExpirationDate).IsRequired(false);

        builder.HasIndex(x => new {x.UserName, x.Email});
    }
}