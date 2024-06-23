using ClickNPick.Application.Common;
using ClickNPick.Application.DeliveryModels;
using ClickNPick.Application.DeliveryModels.Request;
using ClickNPick.Application.DtoModels.Delivery.Response;

namespace ClickNPick.Web.Models.Delivery.Response;

public class ShipmentDetailsResponseModel
{
    public string ShipmentType { get; set; }

    public int? PackCount { get; set; }

    public string ShipmentDescription { get; set; }

    public double? Weight { get; set; }

    public string SenderDeliveryType { get; set; }

    public ClientProfile? SenderClient { get; set; }

    public string SenderOfficeCode { get; set; }

    public string ReceiverDeliveryType { get; set; }

    public ClientProfile? ReceiverClient { get; set; }

    public double? TotalPrice { get; set; }

    public double? SenderDueAmount { get; set; }

    public List<ShipmentStatusService>? Services { get; set; }

    public List<ShipmentTrackingEvent>? TrackingEvents { get; set; }

    public string ExpectedDeliveryDate { get; set; }

    public string CourierStatus { get; set; }

    public static ShipmentDetailsResponseModel FromShipmentDetailsResponseDto(ShipmentDetailsResponseDto dto)
    {
        var model = new ShipmentDetailsResponseModel();

        model.ShipmentType = dto.ShipmentType;
        model.PackCount = dto.PackCount;
        model.ShipmentDescription = dto.ShipmentDescription;
        model.Weight = dto.Weight;
        model.SenderDeliveryType = dto.SenderDeliveryType;
        model.SenderClient = dto.SenderClient;
        model.ReceiverDeliveryType = dto.ReceiverDeliveryType;
        model.ReceiverClient = dto.ReceiverClient;
        model.TotalPrice = dto.TotalPrice;
        model.SenderDueAmount = dto.SenderDueAmount;
        model.Services = dto.Services;
        model.TrackingEvents = dto.TrackingEvents;
        model.ExpectedDeliveryDate = dto.ExpectedDeliveryDate;
        model.CourierStatus = dto.CourierStatus;
        model.SenderOfficeCode = dto.SenderOfficeCode;
        
        return model;
    }
}
