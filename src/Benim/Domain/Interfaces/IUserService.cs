using Benim.Features.User.Commands;

namespace Benim.Domain.Interfaces;

public interface IUserService
{
    Task<int> RegisterUserAsync(RegisterUserCommand registerUser);
}