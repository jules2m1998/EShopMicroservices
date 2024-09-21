namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders) =>
        orders.Select(x => new OrderDto(
            Id: x.Id.Value,
            CustomerId: x.CustomerId.Value,
            OrderName: x.OrderName.Value,
            ShippingAddress: new AddressDto(
                x.ShippingAddress.FirstName,
                x.ShippingAddress.LastName,
                x.ShippingAddress.EmailAddress ?? "",
                x.ShippingAddress.AddressLine,
                x.ShippingAddress.Country,
                x.ShippingAddress.State,
                x.ShippingAddress.ZipCode
            ),
            BillingAddress: new AddressDto(
                x.BillingAddress.FirstName,
                x.BillingAddress.LastName,
                x.BillingAddress.EmailAddress ?? "",
                x.BillingAddress.AddressLine,
                x.BillingAddress.Country,
                x.BillingAddress.State,
                x.BillingAddress.ZipCode
            ),
            Payment: new PaymentDto(
                x.Payment.CardName ?? "",
                x.Payment.CartNumber,
                x.Payment.Expiration,
                x.Payment.CVV,
                x.Payment.PaymentMethod
            ),
            Status: x.Status,
            OrderItems: x.OrderItems.Select(oi => new OrderItemDto(
                    oi.OrderId.Value,
                    oi.ProductId.Value,
                    oi.Quantity,
                    oi.Price
                ))
                .ToList()
        ));

    public static OrderDto ToOrderDto(this Order order) => DtoFromOrder(order);

    private static OrderDto DtoFromOrder(Order order) =>
        new(
            Id: order.Id.Value,
            CustomerId: order.CustomerId.Value,
            OrderName: order.OrderName.Value,
            ShippingAddress: new AddressDto(
                order.ShippingAddress.FirstName,
                order.ShippingAddress.LastName,
                order.ShippingAddress.EmailAddress!,
                order.ShippingAddress.AddressLine,
                order.ShippingAddress.Country,
                order.ShippingAddress.State,
                order.ShippingAddress.ZipCode
            ),
            BillingAddress: new AddressDto(
                order.BillingAddress.FirstName,
                order.BillingAddress.LastName,
                order.BillingAddress.EmailAddress!,
                order.BillingAddress.AddressLine,
                order.BillingAddress.Country,
                order.BillingAddress.State,
                order.BillingAddress.ZipCode
            ),
            Payment: new PaymentDto(
                order.Payment.CardName!,
                order.Payment.CartNumber,
                order.Payment.Expiration,
                order.Payment.CVV,
                order.Payment.PaymentMethod
            ),
            Status: order.Status,
            OrderItems: order
                .OrderItems.Select(oi => new OrderItemDto(
                    oi.OrderId.Value,
                    oi.ProductId.Value,
                    oi.Quantity,
                    oi.Price
                ))
                .ToList()
        );
}
