using Basket.Api.Data;

namespace Basket.Api.Basket.GetBacket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

public class GetBacketQueryHandler(IBasketRepository repository)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(
        GetBasketQuery query,
        CancellationToken cancellationToken
    )
    {
        var basket = await repository.GetBasket(query.UserName, cancellationToken);
        return new(basket);
    }
}
