namespace ClickNPick.Application.Exceptions.Categories;

public class CategoryNotFoundException : Exception
{
    private const string DefaultMessage = "Category not found.";

    public CategoryNotFoundException() : base(DefaultMessage) { }

    public CategoryNotFoundException(string message) : base(message) { }
}
