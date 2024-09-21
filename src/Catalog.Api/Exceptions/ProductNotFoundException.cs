namespace Catalog.Api.Exceptions;

public class ProductNotFoundException(Guid Id) : NotFoundException("Product", Id) { }
