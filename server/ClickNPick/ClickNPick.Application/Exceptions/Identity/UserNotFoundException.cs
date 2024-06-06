namespace ClickNPick.Application.Exceptions.Identity;

public class UserNotFoundException : Exception
{
    private const string DefaultMessage = "User is not found.";

    public UserNotFoundException() : base(DefaultMessage) { }

    public UserNotFoundException(string message) : base(message) { }
}
