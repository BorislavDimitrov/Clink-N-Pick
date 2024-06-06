namespace ClickNPick.Application.Exceptions.General;

public class OperationFailedException : Exception
{
    private const string DefaultMessage = "The operation is not completed successfully.";

    public OperationFailedException() : base(DefaultMessage) { }

    public OperationFailedException(string message) : base(message) { }
}
