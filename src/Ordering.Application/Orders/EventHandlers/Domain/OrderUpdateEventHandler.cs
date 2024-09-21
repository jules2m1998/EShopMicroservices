namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderUpdateEventHandler(ILogger<OrderUpdateEvent> logger)
    : INotificationHandler<OrderUpdateEvent>
{
    public Task Handle(OrderUpdateEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handler: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
