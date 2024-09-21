using Basket.Api.Data;

namespace Basket.Api.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasket : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasket()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
    }
}

public class DeleteBasketHandler(IBasketRepository repository)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(
        DeleteBasketCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await repository.DeleteBasket(request.UserName, cancellationToken);
        return new(result);
    }
}
