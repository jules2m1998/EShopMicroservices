namespace Basket.Api.Basket.DeleteBasket;

public record DeleteBasketRequest(string UserName);

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                "/basket/{userName}",
                async (string userName, ISender sender, CancellationToken cancellationToken) =>
                {
                    var query = new DeleteBasketCommand(userName);
                    var result = await sender.Send(query, cancellationToken);

                    return Results.Ok(result.Adapt<DeleteBasketResponse>());
                }
            )
            .WithName("DeleteBasket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete basket by user name")
            .WithDescription("Delete basket by user name");
    }
}
