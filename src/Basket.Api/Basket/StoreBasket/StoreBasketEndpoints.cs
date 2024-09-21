namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);

public record StoreBasketResponse(string UserName);

public class StoreBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/basket",
                async (
                    StoreBasketRequest request,
                    ISender sender,
                    CancellationToken cancellationToken
                ) =>
                {
                    var query = request.Adapt<StoreBasketCommand>();
                    var result = await sender.Send(query, cancellationToken);
                    var response = result.Adapt<StoreBasketResponse>();

                    return Results.CreatedAtRoute(
                        "GetBasketByUser",
                        new { response.UserName },
                        response
                    );
                }
            )
            .WithName("StoreBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store basket")
            .WithDescription("Store basket");
    }
}
