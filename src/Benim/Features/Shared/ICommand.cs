using MediatR;

namespace Benim.Features.Shared;

public interface ICommand<out TResponse>:IRequest<TResponse>
{
    
}