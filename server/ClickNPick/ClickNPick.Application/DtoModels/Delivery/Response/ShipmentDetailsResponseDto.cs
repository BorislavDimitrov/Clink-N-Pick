using ClickNPick.Application.Common;
using ClickNPick.Application.DeliveryModels;
using ClickNPick.Application.DeliveryModels.Request;

namespace ClickNPick.Application.DtoModels.Delivery.Response;

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

    public string CourierStatus { get; set; }

    public static ShipmentDetailsResponseDto FromShipmentStatus(ShipmentStatus shipmentStatus)
    {
        var dto = new ShipmentDetailsResponseDto();

        dto.ShipmentType = shipmentStatus.ShipmentType;
        dto.PackCount = shipmentStatus.PackCount;
        dto.ShipmentDescription = shipmentStatus.ShipmentDescription;
        dto.Weight = shipmentStatus.Weight;
        dto.SenderDeliveryType = shipmentStatus.SenderDeliveryType;
        dto.SenderClient = shipmentStatus.SenderClient;
        dto.ReceiverDeliveryType = shipmentStatus.ReceiverDeliveryType;
        dto.ReceiverClient = shipmentStatus.ReceiverClient;
        dto.TotalPrice = shipmentStatus.TotalPrice;
        dto.SenderDueAmount = shipmentStatus.SenderDueAmount;
        dto.Services = shipmentStatus.Services;
        dto.TrackingEvents = shipmentStatus.TrackingEvents;
        dto.ExpectedDeliveryDate = shipmentStatus.ExpectedDeliveryDate != null ? DateTimeOffset
            .FromUnixTimeMilliseconds(shipmentStatus.ExpectedDeliveryDate)
            .LocalDateTime
            .ToString("F") : string.Empty;

        return dto;
    }
}

