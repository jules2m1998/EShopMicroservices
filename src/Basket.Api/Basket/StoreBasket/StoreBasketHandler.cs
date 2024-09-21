using Basket.Api.Data;
using static Discount.Grpc.DiscountProtoService;

namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("User name is required");
    }
}

public class StoreBasketHandler(
    IBasketRepository repository,
    DiscountProtoServiceClient discountProto
) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(
        StoreBasketCommand request,
        CancellationToken cancellationToken
    )
    {
        var cart = request.Cart;
        await DeductDiscount(cart, cancellationToken);
        var result = await repository.StoreBasket(cart, cancellationToken);

        return new(result.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(
                new() { ProductName = item.ProductName },
                cancellationToken: cancellationToken
            );
            item.Price -= coupon.Amount;
        }
    }
}
