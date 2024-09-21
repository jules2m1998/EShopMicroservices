namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price
) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is required");
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .Length(2, 150)
            .WithMessage("Name must be between 2 and 150 characters");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

internal class UpdateProductCommandHander(
    IDocumentSession session,
    ILogger<UpdateProductCommandHander> logger
) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(
        UpdateProductCommand command,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("UpdateProductCommandHander.Handler called with {@command}", command);
        var product =
            await session.LoadAsync<Product>(command.Id, cancellationToken)
            ?? throw new ProductNotFoundException(command.Id);
        product.Name = command.Name;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
        product.Categories = command.Categories;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
