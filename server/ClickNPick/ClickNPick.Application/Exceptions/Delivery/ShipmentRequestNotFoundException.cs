namespace ClickNPick.Application.Exceptions.Delivery
{
    public class ShipmentRequestNotFoundException : Exception
    {
        private const string DefaultMessage = "Shipment not found.";

        public ShipmentRequestNotFoundException() : base(DefaultMessage) { }

        public ShipmentRequestNotFoundException(string message) : base(message) { }
    }
}
