using ClickNPick.Application.DtoModels.Products.Response;
using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Delivery.Response
{
    public class ShipmentListingResponseDto
    {
        public List<ShipmentInListResponseDto> Shipments { get; set; }

        public static ShipmentListingResponseDto FromShipmentRequests(IEnumerable<ShipmentRequest> shipmentRequests)
        {
            var shipmentsDto = shipmentRequests.Select(x => ShipmentInListResponseDto.FromShipmentRequest(x)).ToList();

            return new ShipmentListingResponseDto { Shipments = shipmentsDto };
        }
    }
}
