using ClickNPick.Application.Common;
using ClickNPick.Application.DeliveryModels;
using ClickNPick.Application.DeliveryModels.Request;
using ClickNPick.Application.DtoModels.Delivery.Response;

namespace ClickNPick.Web.Models.Delivery.Response
{
    public class ShipmentDetailsResponseModel
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

        public static ShipmentDetailsResponseModel FromShipmentDetailsResponseDto(ShipmentDetailsResponseDto dto)
        {
            return new ShipmentDetailsResponseModel
            {
                ShipmentType = dto.ShipmentType,
                PackCount = dto.PackCount,
                ShipmentDescription = dto.ShipmentDescription,
                Weight = dto.Weight,
                SenderDeliveryType = dto.SenderDeliveryType,
                SenderClient = dto.SenderClient,
                ReceiverDeliveryType = dto.ReceiverDeliveryType,
                ReceiverClient = dto.ReceiverClient,
                TotalPrice = dto.TotalPrice,
                SenderDueAmount = dto.SenderDueAmount,
                Services = dto.Services,
                TrackingEvents = dto.TrackingEvents,
                ExpectedDeliveryDate = dto.ExpectedDeliveryDate
            };
        }
    }
}
