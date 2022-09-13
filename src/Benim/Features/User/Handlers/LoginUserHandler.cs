using Benim.Features.Shared;
using Benim.Features.User.Commands;

namespace Benim.Features.User.Handlers;

public class LoginUserHandler: ICommandHandler<LoginUserCommand,LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = new LoginResponse();
        var userName = request.UserName;
        var password = request.Password;

        if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
        {
            result.AddError("Invalid UserName Or Password!");
            return result;
        }

        if (string.IsNullOrEmpty(userName))
        {
            result.AddError("Invalid UserName!");
            return result;
        }

        if (string.IsNullOrEmpty(password))
        {
            result.AddError("Invalid Password!");
            return result;
        }

        if (userName != "admin" && password != "Admin@1234")
        {
            result.AddError("Invalid UserName Or Password!");
            return result;
        }

        return result;
    }
}