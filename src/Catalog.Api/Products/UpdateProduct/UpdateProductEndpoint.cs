using Catalog.Api.Products.CreateProduct;

namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductRequest(
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price
);

public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut(
                "/products/{id:guid}",
                async (
                    Guid id,
                    UpdateProductRequest product,
                    ISender sender,
                    CancellationToken cancelationToken
                ) =>
                {
                    var command = new UpdateProductCommand(
                        id,
                        product.Name,
                        product.Categories,
                        product.Description,
                        product.ImageFile,
                        product.Price
                    );
                    var result = await sender.Send(command, cancelationToken);

                    return result.Adapt<UpdateProductResponse>();
                }
            )
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithDescription("Update description");
    }
}
