namespace BuildingBlocks.Exceptions;

public class InternalServerException(string message) : Exception(message)
{
    public string? Details { get; }

    public InternalServerException(string message, string details)
        : this(message)
    {
        Details = details;
    }
}
