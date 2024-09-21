namespace Basket.Api.Basket.GetBacket;

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/basket/{userName}",
                async (string userName, ISender sender, CancellationToken cancellationToken) =>
                {
                    var query = new GetBasketQuery(userName);
                    var result = await sender.Send(query, cancellationToken);

                    return Results.Ok(result.Adapt<GetBasketResponse>());
                }
            )
            .WithName("GetBasketByUser")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket by user name")
            .WithDescription("Get Basket");
    }
}
