using ClickNPick.Application.Exceptions.General;

namespace ClickNPick.Application.Exceptions.Delivery;

public class ShipmentRequestNotFoundException : NotFoundException
{
    private const string DefaultMessage = "Shipment not found.";

    public ShipmentRequestNotFoundException() : base(DefaultMessage) { }

    public ShipmentRequestNotFoundException(string message) : base(message) { }
}
