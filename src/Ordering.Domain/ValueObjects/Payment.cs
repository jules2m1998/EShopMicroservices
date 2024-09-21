namespace Ordering.Domain.ValueObjects;

public class Payment
{
    public string? CardName { get; } = default!;
    public string CartNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string CVV { get; } = default!;
    public int PaymentMethod { get; } = default!;

    protected Payment() { }

    private Payment(
        string? cardName,
        string cartNumber,
        string expiration,
        string cVV,
        int paymentMethod
    )
    {
        CardName = cardName;
        CartNumber = cartNumber;
        Expiration = expiration;
        CVV = cVV;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(
        string? cardName,
        string cartNumber,
        string expiration,
        string cVV,
        int paymentMethod
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName, nameof(cardName));
        ArgumentException.ThrowIfNullOrWhiteSpace(cartNumber, nameof(cartNumber));
        ArgumentException.ThrowIfNullOrWhiteSpace(cVV, nameof(cVV));
        ArgumentOutOfRangeException.ThrowIfNotEqual(cVV.Length, 3);

        return new(cardName, cartNumber, expiration, cVV, paymentMethod);
    }
}
