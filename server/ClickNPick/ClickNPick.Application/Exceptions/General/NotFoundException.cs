namespace ClickNPick.Application.Exceptions.General;

public class NotFoundException : Exception
{
    private const string DefaultMessage = "The resource was not found.";

    public NotFoundException() : base(DefaultMessage) { }

    public NotFoundException(string message) : base(message) { }
}
