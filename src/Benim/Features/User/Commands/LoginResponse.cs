namespace Benim.Features.User.Commands;

public class LoginResponse
{
    public LoginResponse(string userName,string fullName, string email,string token,string refreshToken, DateTime expireDateTime)
    {
        UserName = userName;
        FullName = fullName;
        Email = email;
        Token = token;
        RefreshToken = refreshToken;
        ExpireDateTime = expireDateTime;
    }
    public string UserName { get; }
    public string FullName { get; }
    public string Email { get; }
    public string Token { get; } 
    public string RefreshToken { get; }
    public DateTime ExpireDateTime { get; }
}