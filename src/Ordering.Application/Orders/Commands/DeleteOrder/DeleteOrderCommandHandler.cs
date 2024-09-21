namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(
        DeleteOrderCommand request,
        CancellationToken cancellationToken
    )
    {
        var orderId = OrderId.Of(request.OrderId);
        var order =
            await dbContext.Orders.FindAsync([orderId], cancellationToken)
            ?? throw new OrderNotFoundException(request.OrderId);
        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new(true);
    }
}
