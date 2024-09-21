using Catalog.Api.Exceptions;

namespace Catalog.Api.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(
    IDocumentSession session,
    ILogger<GetProductByCategoryQueryHandler> logger
) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(
        GetProductByCategoryQuery query,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation(
            "GetProductByCategoryQueryHandler.Handle called with {@Query}",
            query
        );
        var products = await session
            .Query<Product>()
            .Where(x => x.Categories.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new(products);
    }
}
