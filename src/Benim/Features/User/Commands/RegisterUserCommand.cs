using Benim.Features.Shared;

namespace Benim.Features.User.Commands;

public class RegisterUserCommand: ICommand<int>
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}