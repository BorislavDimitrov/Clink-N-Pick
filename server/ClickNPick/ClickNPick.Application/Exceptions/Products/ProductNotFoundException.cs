namespace ClickNPick.Application.Exceptions.Products;

internal class ProductNotFoundException : Exception
{
    private const string DefaultMessage = "Product not found";

    public ProductNotFoundException() : base(DefaultMessage) { }

    public ProductNotFoundException(string message) : base(message) { }
}
