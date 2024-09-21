using Catalog.Api.Products.GetProducts;

namespace Catalog.Api.Products.GetProductById;

public record GetProductByIdResponse(Product Product);

public class GetProductByIdendpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products/{id:guid}",
                async (Guid id, ISender sender, CancellationToken cancellation) =>
                {
                    var result = await sender.Send(new GetProductByIdQuery(id), cancellation);
                    if (result is null)
                        return Results.NotFound();

                    return Results.Ok(result.Adapt<GetProductByIdResponse>());
                }
            )
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by id")
            .WithDescription("Get product by id");
    }
}
