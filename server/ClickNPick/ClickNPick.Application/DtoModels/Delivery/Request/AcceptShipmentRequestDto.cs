namespace ClickNPick.Application.DtoModels.Delivery.Request
{
    public class AcceptShipmentRequestDto
    {
        public string RequestShipmentId { get; set; }

        public string SenderName { get; set; }

        public string SenderPhoneNumber { get; set; }

        public DateTime SendDate { get; set; }

        public int PackCount { get; set; }

        public double PaymentReceiverAmount { get; set; }

        public string ShipmentType { get; set; }

        public double Weight { get; set; }

        public string ShipmentDescription { get; set; }

        public string OrderNumber { get; set; }

        public string? SenderOfficeCode { get; set; }

        public string UserId { get; set; }
    }
}
