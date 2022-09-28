using Benim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benim.Infrastructure.Data.Configs;

public class UserConfiguration: IEntityTypeConfiguration<User<int>>
{
    public void Configure(EntityTypeBuilder<User<int>> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.BirthDate).IsRequired(false);

        builder.HasIndex(x => new {x.UserName, x.Email});
    }
}