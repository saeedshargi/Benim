using Microsoft.AspNetCore.Identity;

namespace Benim.Domain.Entities;

public sealed class Role:IdentityRole<int>
{
    private Role(string name,string? description)
    {
        Name = name;
        NormalizedName = name.ToUpper();
        ConcurrencyStamp = Guid.NewGuid().ToString();
        Description = description;
        IsActive = true;
        IsDeleted = false;
    }

    public Role()
    {
        
    }
    
    public string? Description { get;}
    public bool IsActive { get; }
    public bool IsDeleted { get;}

    public static Role CreateRole(string name, string? description)
    {
        return new Role(name, description);
    }
}