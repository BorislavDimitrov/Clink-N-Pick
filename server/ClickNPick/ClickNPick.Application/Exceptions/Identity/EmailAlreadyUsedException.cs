namespace ClickNPick.Application.Exceptions.Identity;

public class EmailAlreadyUsedException : Exception
{
    private const string DefaultMessage = "The email is already in use.";

    public EmailAlreadyUsedException() : base(DefaultMessage) { }

    public EmailAlreadyUsedException(string message) : base(message) { }
}
