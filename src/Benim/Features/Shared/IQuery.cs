using MediatR;

namespace Benim.Features.Shared;

public interface IQuery<out TResponse>: IRequest<TResponse>
{
    
}