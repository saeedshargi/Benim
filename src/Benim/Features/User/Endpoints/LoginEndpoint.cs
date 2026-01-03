using Benim.Features.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Benim.Features.User.Endpoints;

public static class LoginEndpoint
{
    internal static RouteHandlerBuilder MapLoginEndpoint(this IEndpointRouteBuilder endpoint)
    {
        return endpoint
            .MapPost("/Login", Login)
            .WithName("Login");
    }

    private static async Task<IResult> Login([FromBody] LoginUserCommand loginUserCommand, IMediator mediator, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(loginUserCommand, cancellationToken);

        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }
}