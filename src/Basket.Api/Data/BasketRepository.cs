﻿namespace Basket.Api.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<bool> DeleteBasket(
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        session.Delete<ShoppingCart>(userName);
        await session.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<ShoppingCart> GetBasket(
        string userName,
        CancellationToken cancellationToken = default
    ) =>
        await session.LoadAsync<ShoppingCart>(userName, cancellationToken)
        ?? throw new BasketNotFoundException(userName);

    public async Task<ShoppingCart> StoreBasket(
        ShoppingCart basket,
        CancellationToken cancellationToken = default
    )
    {
        session.Store(basket);
        await session.SaveChangesAsync(cancellationToken);

        return basket;
    }
}
