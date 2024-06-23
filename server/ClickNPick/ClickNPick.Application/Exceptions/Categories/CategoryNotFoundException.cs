using ClickNPick.Application.Exceptions.General;

namespace ClickNPick.Application.Exceptions.Categories;

public class CategoryNotFoundException : NotFoundException
{
    private const string DefaultMessage = "Category not found.";

    public CategoryNotFoundException() : base(DefaultMessage) { }

    public CategoryNotFoundException(string message) : base(message) { }
}
