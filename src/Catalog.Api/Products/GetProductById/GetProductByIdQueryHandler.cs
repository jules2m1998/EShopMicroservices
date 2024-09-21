using Catalog.Api.Exceptions;

namespace Catalog.Api.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdsResult>;

public record GetProductByIdsResult(Product Product) : IQuery;

internal class GetProductByIdQueryHandler(
    IDocumentSession session,
    ILogger<GetProductByIdQueryHandler> logger
) : IQueryHandler<GetProductByIdQuery, GetProductByIdsResult>
{
    public async Task<GetProductByIdsResult> Handle(
        GetProductByIdQuery query,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", query);
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
        return product is null ? throw new ProductNotFoundException(query.Id) : new(product);
    }
}
