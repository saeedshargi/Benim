using Benim.Domain.Common;
using Benim.Features.Shared;

namespace Benim.Features.User.Commands;

public class LoginUserCommand : ICommand<Result<LoginResponse>>
{
    public LoginUserCommand(string? userName,string? password,bool rememberMe = false)
    {
        UserName = userName;
        Password = password;
        RememberMe = rememberMe;
    }

    public string? UserName { get; }
    public string? Password { get; }
    public bool RememberMe { get; }
}