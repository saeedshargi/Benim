using MediatR;

namespace Benim.Features.Shared;

public interface ICommandHandler<in  TCommand,TResponse>: IRequestHandler<TCommand,TResponse> where TCommand : ICommand<TResponse>
{
    
}