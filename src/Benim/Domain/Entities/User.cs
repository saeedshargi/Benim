using Microsoft.AspNetCore.Identity;

namespace Benim.Domain.Entities;

public sealed class User<T>: IdentityUser<int>
{
    private User(string userName,string password,string firstName,string lastName,DateTime? birthDate,string? email)
    {
        UserName = userName;
        PasswordHash = password;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Email = email;
        IsActive = true;
        IsDeleted = false;
    }

    public User()
    {
        
    }

    public string FirstName { get; }
    public string LastName { get; }
    public DateTime? BirthDate { get; }
    public bool IsActive { get; }
    public bool IsDeleted { get; }

    public User<T> CreateUser(string userName, string password, string firstName, string lastName, DateTime? birthDate, string? email)
    {
        return new User<T>(userName, password, firstName, lastName, birthDate, email);
    }
}