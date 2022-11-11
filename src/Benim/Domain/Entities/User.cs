using Microsoft.AspNetCore.Identity;

namespace Benim.Domain.Entities;

public sealed class User: IdentityUser<int>
{
    private string? _refreshToken;
    private DateTime? _refreshTokenExpirationDate;
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
    public string? RefreshToken => _refreshToken;
    public DateTime? RefreshTokenExpirationDate => _refreshTokenExpirationDate;

    public static User CreateUser(string userName, string password, string firstName, string lastName, DateTime? birthDate, string? email)
    {
        return new User(userName, password, firstName, lastName, birthDate, email);
    }

    public void UpdateRefreshToken(string? refreshToken, DateTime? refreshTokenExpirationDateTime)
    {
        _refreshToken = refreshToken;
        _refreshTokenExpirationDate = refreshTokenExpirationDateTime;
    }
}