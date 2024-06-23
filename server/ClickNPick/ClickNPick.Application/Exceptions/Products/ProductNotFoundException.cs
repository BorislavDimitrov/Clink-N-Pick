using ClickNPick.Application.Exceptions.General;

namespace ClickNPick.Application.Exceptions.Products;

public class ProductNotFoundException : NotFoundException
{
    private const string DefaultMessage = "Product not found";

    public ProductNotFoundException() : base(DefaultMessage) { }

    public ProductNotFoundException(string message) : base(message) { }
}
