using MediatR;

namespace Benim.Domain.Common;

public abstract class DomainEventBase: INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}