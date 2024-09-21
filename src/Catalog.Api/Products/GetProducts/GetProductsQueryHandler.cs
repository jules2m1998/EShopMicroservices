using Marten.Pagination;

namespace Catalog.Api.Products.GetProducts;

public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsQueryHandler(
    IDocumentSession session,
    ILogger<GetProductsQueryHandler> logger
) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(
        GetProductsQuery query,
        CancellationToken cancellationToken
    )
    {
        var name = nameof(GetProductsQueryHandler);
        var method = nameof(Handle);

        logger.LogInformation("{name}.{method} called with {query}", name, method, query);
        var products = await session
            .Query<Product>()
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new(products);
    }
}
