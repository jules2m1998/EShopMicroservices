namespace Catalog.Api.Products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/products/category/{name}",
            async (string name, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(name));
                return Results.Ok(result.Adapt<GetProductByCategoryResponse>());
            }
        );
    }
}
