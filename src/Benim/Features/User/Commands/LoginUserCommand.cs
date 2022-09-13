using Benim.Features.Shared;

namespace Benim.Features.User.Commands;

public class LoginUserCommand : ICommand<LoginResponse>
{
    public LoginUserCommand(string? userName,string? password)
    {
        UserName = userName;
        Password = password;
    }

    public string? UserName { get; }
    public string? Password { get; }
}