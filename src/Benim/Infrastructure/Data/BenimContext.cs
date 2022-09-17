using System.Reflection;
using Benim.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Benim.Infrastructure.Data;

public class BenimContext : IdentityDbContext<IdentityUser<int>,IdentityRole<int>,int>
{
    public BenimContext(DbContextOptions<BenimContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<IdentityUser<int>>(c => c.ToTable("Users"));
        builder.Entity<IdentityUserClaim<int>>(c => c.ToTable("UserClaims"));
        builder.Entity<IdentityUserLogin<int>>(c => c.ToTable("UserLogins"));
        builder.Entity<IdentityUserToken<int>>(c => c.ToTable("UserTokens"));
        builder.Entity<IdentityRole<int>>(c => c.ToTable("Roles"));
        builder.Entity<IdentityRoleClaim<int>>(c => c.ToTable("RoleClaims"));
        builder.Entity<IdentityUserRole<int>>(c => c.ToTable("UserRoles"));
    }
}