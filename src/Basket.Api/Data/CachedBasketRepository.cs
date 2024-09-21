using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Api.Data;

public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache)
    : IBasketRepository
{
    public async Task<ShoppingCart> StoreBasket(
        ShoppingCart basket,
        CancellationToken cancellationToken = default
    )
    {
        var data = await repository.StoreBasket(basket, cancellationToken);
        await cache.SetStringAsync(
            basket.UserName,
            JsonSerializer.Serialize(data),
            cancellationToken
        );

        return data;
    }

    public async Task<ShoppingCart> GetBasket(
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
        var deserialised = !string.IsNullOrEmpty(cachedBasket)
            ? JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)
            : null;
        if (deserialised is not null)
        {
            return deserialised;
        }
        var basket = await repository.GetBasket(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<bool> DeleteBasket(
        string userName,
        CancellationToken cancellationToken = default
    )
    {
        var result = await repository.DeleteBasket(userName, cancellationToken);
        await cache.RemoveAsync(userName, cancellationToken);
        return result;
    }
}
