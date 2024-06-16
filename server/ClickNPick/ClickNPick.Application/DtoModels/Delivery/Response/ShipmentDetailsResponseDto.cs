using ClickNPick.Application.Common;
using ClickNPick.Application.DeliveryModels;
using ClickNPick.Application.DeliveryModels.Request;

namespace ClickNPick.Application.DtoModels.Delivery.Response
{
    public class ShipmentDetailsResponseDto
    {
        public string? ShipmentType { get; set; }

        public int? PackCount { get; set; }

        public string? ShipmentDescription { get; set; }

        public double? Weight { get; set; }

        public string? SenderDeliveryType { get; set; }

        public ClientProfile? SenderClient { get; set; }

        public string? SenderOfficeCode { get; set; }

        public string? ReceiverDeliveryType { get; set; }

        public ClientProfile? ReceiverClient { get; set; }

        public double? TotalPrice { get; set; }

        public double? SenderDueAmount { get; set; }

        public List<ShipmentStatusService>? Services { get; set; }

        public List<ShipmentTrackingEvent>? TrackingEvents { get; set; }

        public string ExpectedDeliveryDate { get; set; }

        public RequestCourierStatus CourierStatus { get; set; }

        public static ShipmentDetailsResponseDto FromShipmentStatus(ShipmentStatus shipmentStatus)
        {
            return new ShipmentDetailsResponseDto
            {
                ShipmentType = shipmentStatus.ShipmentType,
                PackCount = shipmentStatus.PackCount,
                ShipmentDescription = shipmentStatus.ShipmentDescription,
                Weight = shipmentStatus.Weight,
                SenderDeliveryType = shipmentStatus.SenderDeliveryType,
                SenderClient = shipmentStatus.SenderClient,
                ReceiverDeliveryType = shipmentStatus.ReceiverDeliveryType,
                ReceiverClient = shipmentStatus.ReceiverClient,
                TotalPrice = shipmentStatus.TotalPrice,
                SenderDueAmount = shipmentStatus.SenderDueAmount,
                Services = shipmentStatus.Services,
                TrackingEvents = shipmentStatus.TrackingEvents,
                ExpectedDeliveryDate = shipmentStatus.ExpectedDeliveryDate != null ? DateTimeOffset
                    .FromUnixTimeMilliseconds(shipmentStatus.ExpectedDeliveryDate)
                    .LocalDateTime
                    .ToString("F") : string.Empty};
        }
    }
}

