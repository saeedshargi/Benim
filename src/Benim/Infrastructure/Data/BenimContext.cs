using System.Reflection;
using Benim.Domain;
using Benim.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Benim.Infrastructure.Data;

public class BenimContext : IdentityDbContext<User<int>,Role<int>,int>
{
    public BenimContext(DbContextOptions<BenimContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityUserClaim<int>>(c => c.ToTable("UserClaims"));
        builder.Entity<IdentityUserLogin<int>>(c => c.ToTable("UserLogins"));
        builder.Entity<IdentityUserToken<int>>(c => c.ToTable("UserTokens"));
        builder.Entity<IdentityRoleClaim<int>>(c => c.ToTable("RoleClaims"));
        builder.Entity<IdentityUserRole<int>>(c => c.ToTable("UserRoles"));
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}